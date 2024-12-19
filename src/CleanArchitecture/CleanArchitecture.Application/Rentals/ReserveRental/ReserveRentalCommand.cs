using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Rentals.ReserveRental
{
    public record ReserveRentalCommand(
        Guid vehicleId,
        Guid userId,
        DateOnly StartDate,
        DateOnly EndDate
    ) : ICommand<Guid>;


}