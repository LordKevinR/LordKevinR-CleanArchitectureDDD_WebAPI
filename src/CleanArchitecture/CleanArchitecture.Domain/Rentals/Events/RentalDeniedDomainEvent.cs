using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events
{
    public sealed record RentalDeniedDomainEvent(Guid RentalId) : IDomainEvent;
}