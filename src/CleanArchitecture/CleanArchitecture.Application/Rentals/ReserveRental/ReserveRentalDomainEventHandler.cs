using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Rentals.Events;
using CleanArchitecture.Domain.Users;
using MediatR;

namespace CleanArchitecture.Application.Rentals.ReserveRental
{
    internal sealed class ReserveRentalDomainEventHandler : INotificationHandler<RentalCreatedDomainEvent>
    {
        private readonly IRentalRepository rentalRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;

        public ReserveRentalDomainEventHandler(
            IRentalRepository rentalRepository,
            IUserRepository userRepository,
            IEmailService emailService
        )
        {
            this.rentalRepository = rentalRepository;
            this.userRepository = userRepository;
            this.emailService = emailService;
        }

        public async Task Handle(RentalCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var rental = await rentalRepository.GetByIdAsync(notification.RentalId, cancellationToken);

            if (rental == null)
            {
                return;
            }

            var user = await userRepository.GetByIdAsync(rental.UserId, cancellationToken);

            if (user == null)
            {
                return;
            }

            await emailService.SendAsync(
                user.Email!,
                "Rental Reserved",
                $"Your rental with id {rental.Id} has been reserved."
            );
        }
    }
}