using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rentals.Services
{
    public class PriceService
    {
        public PriceDetail CalculatePrice(Vehicle vehicle, DateRange dateRange)
        {
            CurrencyType currencyType = vehicle.Price!.CurrencyType;

            Currency pricePerPeriod = new Currency(currencyType, dateRange.Days * vehicle.Price.Amount);

            decimal accessoriesPercentage = 0;

            foreach (var Accessorie in vehicle.Accessories!)
            {
                accessoriesPercentage += Accessorie switch
                {
                    Accessorie.Wifi => 0.2m,
                    Accessorie.GPS => 0.1m,
                    Accessorie.AirConditioner => 0.05m,
                    Accessorie.AppleCarplay => 0.05m,
                    Accessorie.Bluetooth => 0.05m,
                    Accessorie.USB => 0.05m,
                    Accessorie.Stereo => 0.1m,
                    Accessorie.Maps => 0.1m,
                    Accessorie.AndroidPay => 0.05m,
                    Accessorie.Radio => 0.05m,
                    _ => 0m
                };

            }

            Currency accessoriesPrice = Currency.Zero(currencyType);

            if (accessoriesPercentage > 0)
            {
                accessoriesPrice = new Currency(currencyType, pricePerPeriod.Amount * accessoriesPercentage);
            }

            var totalPrice = Currency.Zero();
            totalPrice += pricePerPeriod;

            if (!vehicle.Maintenance!.IsZero())
            {
                totalPrice += vehicle.Maintenance;
            }

            totalPrice += accessoriesPrice;

            return new PriceDetail(pricePerPeriod, vehicle.Maintenance, accessoriesPrice, totalPrice);
        }
    }
}