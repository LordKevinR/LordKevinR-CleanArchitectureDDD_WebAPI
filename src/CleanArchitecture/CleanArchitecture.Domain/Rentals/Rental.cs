using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals.Errors;
using CleanArchitecture.Domain.Rentals.Services;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Rentals.Events;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rentals
{
    public sealed class Rental : Entity
    {
        private Rental(
            Guid id,
            Guid vehicleId,
            Guid userId,
            DateRange? duration,
            Currency? pricePerPeriod,
            Currency? maintenance,
            Currency? accessories,
            Currency? totalPrice,
            RentalStatus status,
            DateTime? createdAt
            ) : base(id)
        {
            VehicleId = vehicleId;
            UserId = userId;
            PricePerPeriod = pricePerPeriod;
            Maintenance = maintenance;
            Accessories = accessories;
            TotalPrice = totalPrice;
            Status = status;
            Duration = duration;
            CreatedAt = createdAt;
        }

        public Guid VehicleId { get; private set; }
        public Guid UserId { get; private set; }
        public Currency? PricePerPeriod { get; private set; }
        public Currency? Maintenance { get; private set; }
        public Currency? Accessories { get; private set; }
        public Currency? TotalPrice { get; private set; }
        public RentalStatus Status { get; private set; }
        public DateRange? Duration { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public DateTime? ConfirmedAt { get; private set; }
        public DateTime? DeniedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }

        public static Rental Create(
            Vehicle vehicle,
            Guid userId,
            DateRange duration,
            DateTime createdAt,
            PriceService priceService
        )
        {
            var priceDetail = priceService.CalculatePrice(
                vehicle,
                duration
            );
            Rental rental = new Rental(
                    Guid.NewGuid(),
                    vehicle.Id,
                    userId,
                    duration,
                    priceDetail.PricePerPeriod,
                    priceDetail.Maintenance,
                    priceDetail.Accessories,
                    priceDetail.TotalPrice,
                    RentalStatus.Reserved,
                    createdAt
                );

            rental.AddDomainEvent(new RentalCreatedDomainEvent(rental.Id));

            vehicle.LastRentalDate = createdAt;

            return rental;
        }

        public Result Confirm(DateTime utcNow)
        {
            if (Status != RentalStatus.Reserved)
            {
                return Result.Failure(RentalErrors.NotReserved);
            }

            Status = RentalStatus.Confirmed;
            ConfirmedAt = utcNow;

            AddDomainEvent(new RentalConfirmedDomainEvent(Id));

            return Result.Success();
        }

        public Result Deny(DateTime utcNow)
        {
            if (Status != RentalStatus.Reserved)
            {
                return Result.Failure(RentalErrors.NotReserved);
            }

            Status = RentalStatus.Refused;
            DeniedAt = utcNow;
            AddDomainEvent(new RentalDeniedDomainEvent(Id));

            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if (Status != RentalStatus.Confirmed)
            {
                return Result.Failure(RentalErrors.NotConfirmed);
            }

            var currentDate = DateOnly.FromDateTime(utcNow);
            if (currentDate > Duration!.Start)
            {
                return Result.Failure(RentalErrors.AlreadyStarted);
            }

            Status = RentalStatus.Cancelled;
            CancelledAt = utcNow;
            AddDomainEvent(new RentalCancelledDomainEvent(Id));

            return Result.Success();
        }


        public Result Complete(DateTime utcNow)
        {
            if (Status != RentalStatus.Confirmed)
            {
                return Result.Failure(RentalErrors.NotConfirmed);
            }

            Status = RentalStatus.Completed;
            CompletedAt = utcNow;
            AddDomainEvent(new RentalCompletedDomainEvent(Id));

            return Result.Success();
        }
    }
}