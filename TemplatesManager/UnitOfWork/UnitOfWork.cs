using Db;
using Models;
using Repositorys;

namespace UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly TemplatesDb db;
    public ITemplatesReposytory templatesReposytory { get; }

    public UnitOfWork(TemplatesDb _db, ITemplatesReposytory _templatesReposytory)
    {
        db = _db;
        templatesReposytory = _templatesReposytory;
    }

    public async Task SaveChangesAsync()
    {
        await db.SaveChangesAsync();
    }

}