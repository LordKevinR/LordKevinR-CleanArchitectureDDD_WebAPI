namespace CleanArchitecture.Domain.Vehicles
{
    public record Address(
        string Country,
        string Department,
        string Province,
        string City,
        string Street
    );
}