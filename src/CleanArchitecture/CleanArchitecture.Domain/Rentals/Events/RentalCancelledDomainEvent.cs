using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events
{
    public sealed record RentalCancelledDomainEvent(Guid RentalId) : IDomainEvent;
}