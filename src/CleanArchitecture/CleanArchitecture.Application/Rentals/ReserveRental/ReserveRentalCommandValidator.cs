using FluentValidation;

namespace CleanArchitecture.Application.Rentals.ReserveRental
{
    public class ReserveRentalCommandValidator : AbstractValidator<ReserveRentalCommand>
    {
        public ReserveRentalCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate);
        }
    }
}