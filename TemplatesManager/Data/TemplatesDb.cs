using Microsoft.EntityFrameworkCore;
using Models;

namespace Db;

public class TemplatesDb : DbContext
{
    public TemplatesDb(DbContextOptions options) : base(options) { }
    public DbSet<Template> Templates { get; set; } = null!;
}