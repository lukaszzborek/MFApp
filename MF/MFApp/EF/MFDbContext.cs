using System.ComponentModel;
using MFApp.EF.Converters;
using MFApp.Model;
using MFApp.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace MFApp.EF;

public class MFDbContext : DbContext
{
    public DbSet<NipData> Nips { get; set; }

    public MFDbContext(DbContextOptions<MFDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateOnly>()
                            .HaveConversion<DateOnlyConverter>()
                            .HaveColumnType("date");

        configurationBuilder.Properties<Nip>()
                            .HaveMaxLength(10)
                            .HaveConversion<NipConverter>();
    }
}