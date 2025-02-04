using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicles");
            builder.HasKey(v => v.Id);

            builder.OwnsOne(v => v.Address);

            builder.Property(v => v.Model)
                   .HasMaxLength(200)
                   .HasConversion(model => model!.Value, value => new Model(value));

            builder.Property(v => v.Vin)
                   .HasMaxLength(500)
                   .HasConversion(vin => vin!.Value, value => new Vin(value));

            builder.OwnsOne(v => v.Price, priceBuilder =>
            {
                priceBuilder.Property(currency => currency.CurrencyType)
                            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
            });

            builder.OwnsOne(v => v.Maintenance, maintenanceBuilder =>
            {
                maintenanceBuilder.Property(currency => currency.CurrencyType)
                                  .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
            });

        }
    }
}