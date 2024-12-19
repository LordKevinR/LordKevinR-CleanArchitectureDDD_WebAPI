using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Rentals.Errors;
using CleanArchitecture.Domain.Rentals.Services;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Application.Rentals.ReserveRental;
internal sealed class ReserveRentalCommandHandler : ICommandHandler<ReserveRentalCommand, Guid>
{
    private readonly IUserRepository userRepository;
    private readonly IVehicleRepository vehicleRepository;
    private readonly IRentalRepository rentalRepository;
    private readonly PriceService priceService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IDateTimeProvider dateTimeProvider;

    public ReserveRentalCommandHandler(
        IUserRepository userRepository,
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        PriceService priceService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider
        )
    {
        this.userRepository = userRepository;
        this.vehicleRepository = vehicleRepository;
        this.rentalRepository = rentalRepository;
        this.priceService = priceService;
        this.unitOfWork = unitOfWork;
        this.dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(ReserveRentalCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var vehicle = await vehicleRepository.GetByIdAsync(request.vehicleId, cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await rentalRepository.IsOverlappingAsync(vehicle, duration, cancellationToken))
        {
            return Result.Failure<Guid>(RentalErrors.Overlap);
        }

        var rental = Rental.Create(vehicle, user.Id, duration, dateTimeProvider.currentTime, priceService);

        rentalRepository.Add(rental);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return rental.Id;
    }
}