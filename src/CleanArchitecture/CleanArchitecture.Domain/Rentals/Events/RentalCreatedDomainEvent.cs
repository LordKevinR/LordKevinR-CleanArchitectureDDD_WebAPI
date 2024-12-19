using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events
{
    public sealed record RentalCreatedDomainEvent(Guid RentalId) : IDomainEvent;
}