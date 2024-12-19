namespace CleanArchitecture.Application.Rentals.GetRental
{
    public sealed class RentalResponse
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public Guid VehicleId { get; init; }
        public int Status { get; init; }

        public decimal RentalPrice { get; init; }
        public string? RentalCurrencyType { get; init; }

        public decimal MaintenancePrice { get; init; }
        public string? MaintenanceCurrencyType { get; init; }

        public decimal AccessoriesPrice { get; init; }
        public string? AccessoriesCurrencyType { get; init; }

        public decimal TotalPrice { get; init; }
        public string? TotalCurrencyType { get; init; }

        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }

        public DateTime createdAt { get; init; }
    }
}