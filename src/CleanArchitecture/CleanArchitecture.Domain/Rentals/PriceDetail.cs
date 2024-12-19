using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rentals
{
    public record PriceDetail(Currency PricePerPeriod, Currency Maintenance, Currency Accessories, Currency TotalPrice);
}