namespace CleanArchitecture.Application.Vehicles.SearchVehicles
{
    public sealed class AddressResponse
    {
        public string? Country { get; init; }
        public string? Department { get; init; }
        public string? Province { get; init; }
        public string? Street { get; init; }
    }
}