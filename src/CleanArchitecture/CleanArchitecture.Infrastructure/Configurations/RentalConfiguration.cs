using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rentals");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(r => r.PricePerPeriod, priceBuilder =>
            {
                priceBuilder.Property(currency => currency.CurrencyType)
                            .HasConversion(currencyType => currencyType.Code,
                                           code => CurrencyType.FromCode(code!));
            });

            builder.OwnsOne(r => r.Maintenance, priceBuilder =>
            {
                priceBuilder.Property(currency => currency.CurrencyType)
                            .HasConversion(currencyType => currencyType.Code,
                                           code => CurrencyType.FromCode(code!));
            });

            builder.OwnsOne(r => r.Accessories, priceBuilder =>
            {
                priceBuilder.Property(currency => currency.CurrencyType)
                            .HasConversion(currencyType => currencyType.Code,
                                           code => CurrencyType.FromCode(code!));
            });

            builder.OwnsOne(r => r.TotalPrice, priceBuilder =>
            {
                priceBuilder.Property(currency => currency.CurrencyType)
                            .HasConversion(currencyType => currencyType.Code,
                                           code => CurrencyType.FromCode(code!));
            });

            builder.OwnsOne(r => r.Duration);

            builder.HasOne<Vehicle>()
                   .WithMany()
                   .HasForeignKey(r => r.VehicleId);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(r => r.UserId);
        }
    }
}