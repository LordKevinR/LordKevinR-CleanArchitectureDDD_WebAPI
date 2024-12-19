using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events
{
    public sealed record RentalConfirmedDomainEvent(Guid RentalId) : IDomainEvent;
}