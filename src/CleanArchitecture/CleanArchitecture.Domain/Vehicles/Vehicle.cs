using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehicles
{
    public sealed class Vehicle : Entity
    {
        private Vehicle(
            Guid id,
            Model? model,
            Vin? vin,
            Address? address,
            Currency? price,
            Currency? maintenance,
            DateTime? lastRentalDate,
            List<Accessorie>? accessories
            ) : base(id)
        {
            Model = model;
            Vin = vin;
            Address = address;
            Price = price;
            Maintenance = maintenance;
            LastRentalDate = lastRentalDate;
            Accessories = accessories;
        }
        public Model? Model { get; private set; }
        public Vin? Vin { get; private set; }
        public Address? Address { get; private set; }
        public Currency? Price { get; private set; }
        public Currency? Maintenance { get; private set; }
        public DateTime? LastRentalDate { get; internal set; }
        public List<Accessorie>? Accessories { get; private set; }

        public static Vehicle Create(
            Model? model,
            Vin? vin,
            Address? address,
            Currency? price,
            Currency? maintenance,
            DateTime? lastRentalDate,
            List<Accessorie>? accessories
        )
        {
            var vehicle = new Vehicle(Guid.NewGuid(), model, vin, address, price, maintenance, lastRentalDate, accessories);
            return vehicle;
        }
    }
}