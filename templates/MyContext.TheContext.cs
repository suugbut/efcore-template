using Microsoft.EntityFrameworkCore;
using MyModel.Models;
namespace MyContext;

public class TheContext : DbContext
{
    public TheContext(DbContextOptions<TheContext> options) : base(options) { }
    public DbSet<TheModel> TheModels => Set<TheModel>();
}

