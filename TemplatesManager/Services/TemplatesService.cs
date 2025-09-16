using Models;
using Dtos;
using UoW;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Text.RegularExpressions;

namespace Services;

public class TemplatesService
{
    private readonly IUnitOfWork unitOfWork;

    public TemplatesService(IUnitOfWork _unitOfWork)
    {
        unitOfWork = _unitOfWork;
    }

    public async Task<Result<Template>> Create(TemplateDto dto)
    {
        var templateExist = await unitOfWork.templatesReposytory.GetAll().AnyAsync(t => t.Content == dto.Content);

        if (!templateExist)
        {
            Template template = new Template(dto.Name, dto.Content);
            await unitOfWork.templatesReposytory.AddAsync(template);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(template);
        }
        else
            return Result.Failure<Template>("This template already exists");
    }

    public async Task<Result<Template>> Update(TemplateDto dto, int id)
    {
        var templateExist = await unitOfWork.templatesReposytory.GetAll().FirstOrDefaultAsync(t => t.Id == id);

        if (templateExist != null)
        {
            templateExist.ChangeName(dto.Name);
            templateExist.ChangeContent(dto.Content);

            await unitOfWork.SaveChangesAsync();

            return Result.Success(templateExist);
        }
        else
            return Result.Failure<Template>("There is no such template");
    }

    public async Task<Result<Template>> Delete(int id)
    {
        var templateExist = await unitOfWork.templatesReposytory.GetAll().FirstOrDefaultAsync(t => t.Id == id);

        if (templateExist != null)
        {
            unitOfWork.templatesReposytory.Remove(templateExist);

            await unitOfWork.SaveChangesAsync();
            return Result.Success(templateExist);
        }
        else
            return Result.Failure<Template>("There is no such template");
    }

    public async Task<Result<List<Template>>> GetAll()
    {
        var templateExist = await unitOfWork.templatesReposytory.GetAll().AnyAsync();

        if (templateExist)
        {
            return Result.Success(await unitOfWork.templatesReposytory.GetAll().ToListAsync());
        }
        else
            return Result.Failure<List<Template>>("No templates yet");
    }

    public async Task<Result<byte[]>> GeneratePdfAsync(int templateId, Dictionary<string, string> data)
    {
        var templateExist = await unitOfWork.templatesReposytory.GetAll().FirstOrDefaultAsync(t => t.Id == templateId);

        if (templateExist == null)
        {
            return Result.Failure<byte[]>("There is no such template");
        }

        if (data == null || data.Count == 0)
            return Result.Failure<byte[]>("JSON data is empty");

        foreach (var kv in data)
        {
            if (string.IsNullOrWhiteSpace(kv.Value))
                return Result.Failure<byte[]>($"Field \"{kv.Key}\" is empty");
        }

        string templateContent = templateExist.Content;


        foreach (var pair in data)
        {
            templateContent = templateContent.Replace($"{{{{{pair.Key}}}}}", pair.Value);
        }

        var placeholdersLeft = Regex.Matches(templateContent, @"{{(.*?)}}")
                             .Select(m => m.Groups[1].Value)
                             .Distinct();

        if (placeholdersLeft.Any())
        {
            return Result.Failure<byte[]>("Not all fields are filled in");
        }

        var launchOptions = new LaunchOptions
        {
            Headless = true,
            Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" } 
        };

        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();

        using var browser = await Puppeteer.LaunchAsync(launchOptions);
        using var page = await browser.NewPageAsync();

        await page.SetContentAsync(templateContent);

        var pdfBytes = await page.PdfDataAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true
        });

        return pdfBytes;
    }
}