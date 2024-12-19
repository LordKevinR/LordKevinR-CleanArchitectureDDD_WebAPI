using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events
{
    public sealed record RentalCompletedDomainEvent(Guid RentalId) : IDomainEvent;
}