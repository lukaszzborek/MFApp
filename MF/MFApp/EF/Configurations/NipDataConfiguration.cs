using MFApp.Model;
using MFApp.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFApp.EF.Configurations;

public class NipDataConfiguration : IEntityTypeConfiguration<NipData>
{
    public void Configure(EntityTypeBuilder<NipData> builder)
    {
        builder.HasKey(x => x.Nip);

        builder
            .Property(x => x.Nip)
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(x => x.Regon)
            .HasMaxLength(14)
            .IsRequired(false);

        builder
            .Property(x => x.Pesel)
            .HasMaxLength(11)
            .IsRequired(false);

        builder
            .HasMany(x => x.AuthorizedClerks)
            .WithOne()
            .HasForeignKey(x => new { x.Nip });

        builder
            .HasMany(x => x.Partners)
            .WithOne()
            .HasForeignKey(x => new { x.Nip });

        builder
            .HasMany(x => x.Representatives)
            .WithOne()
            .HasForeignKey(x => new { x.Nip });

        builder
            .HasMany(x => x.AccountNumbers)
            .WithOne()
            .HasForeignKey(x => x.Nip);
    }
}

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => new { x.Nip, x.Type });
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<Partner>(RepresentativeType.Partner)
            .HasValue<AuthorizedClerk>(RepresentativeType.AuthorizedClerk)
            .HasValue<Representative>(RepresentativeType.Representative);
    }
}

public class NipAccountNumberConfiguration : IEntityTypeConfiguration<NipAccountNumber>
{
    public void Configure(EntityTypeBuilder<NipAccountNumber> builder)
    {
        builder.HasKey(x => new { x.Nip, x.Number });
    }
}