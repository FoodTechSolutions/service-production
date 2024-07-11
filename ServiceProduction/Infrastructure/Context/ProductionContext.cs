using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class ProductionContext : DbContext
{
    public ProductionContext(DbContextOptions<ProductionContext> options): base(options)
    {}
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<ProductionProduct> ProductionProducts { get; set; }
    public DbSet<ProductIngredient> ProductsIngredients { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}