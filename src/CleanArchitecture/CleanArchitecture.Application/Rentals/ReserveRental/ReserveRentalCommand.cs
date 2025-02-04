using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Rentals.ReserveRental
{
    public record ReserveRentalCommand(
        Guid VehicleId,
        Guid UserId,
        DateOnly StartDate,
        DateOnly EndDate
    ) : ICommand<Guid>;


}