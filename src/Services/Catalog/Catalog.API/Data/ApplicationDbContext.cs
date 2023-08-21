using Microsoft.EntityFrameworkCore;
using Catalog.API.DTOs;

namespace Catalog.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Item> Items => Set<Item>(); // Returns a DbSet<Item> instance for access to entities of type Item. Reference: https://learn.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext.set?view=entity-framework-6.2.0
}