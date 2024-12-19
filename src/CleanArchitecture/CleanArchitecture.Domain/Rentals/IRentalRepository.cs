using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Rentals
{
    public interface IRentalRepository
    {
        Task<Rental?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsOverlappingAsync(Vehicle vehicle, DateRange dateRange, CancellationToken cancellationToken = default);
        void Add(Rental rental);
    }
}