using Models;

namespace Repositorys;

public interface ITemplatesReposytory
{
    Task AddAsync(Template template);
    IQueryable<Template> GetAll();
    void Remove(Template template);
}