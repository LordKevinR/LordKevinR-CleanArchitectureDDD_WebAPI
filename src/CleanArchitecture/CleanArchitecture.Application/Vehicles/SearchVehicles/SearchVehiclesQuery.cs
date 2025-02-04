using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Vehicles.SearchVehicles
{
    public sealed record SearchVehiclesQuery(
        DateOnly startDate,
        DateOnly endDate
    ) : IQuery<IReadOnlyList<VehicleResponse>>;
}