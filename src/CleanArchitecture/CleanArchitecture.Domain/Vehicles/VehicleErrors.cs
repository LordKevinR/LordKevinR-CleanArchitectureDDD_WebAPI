using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehicles
{
    public class VehicleErrors
    {
        public static readonly Error NotFound = new("Vehicule.NotFound", "The vehicule with that id was not found.");

    }
}