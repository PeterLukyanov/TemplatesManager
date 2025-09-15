using Repositorys;

namespace UoW;

public interface IUnitOfWork
{
    ITemplatesReposytory templatesReposytory{ get; }
    Task SaveChangesAsync();
}