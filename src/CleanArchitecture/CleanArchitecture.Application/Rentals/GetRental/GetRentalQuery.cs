using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Rentals.GetRental
{
    public sealed record GetRentalQuery(Guid RentalId) : IQuery<RentalResponse>;
}