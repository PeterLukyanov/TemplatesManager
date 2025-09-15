using Models;
using Db;
using Repositorys;

public class TemplatesReposytory : ITemplatesReposytory
{
    private readonly TemplatesDb db;

    public TemplatesReposytory(TemplatesDb _db)
    {
        db = _db;
    }

    public IQueryable<Template> GetAll()
    {
        return db.Templates.AsQueryable();
    }

    public async Task AddAsync(Template template)
    {
        await db.Templates.AddAsync(template);
    }

    public void Remove(Template template)
    {
        db.Templates.Remove(template);
    }
}