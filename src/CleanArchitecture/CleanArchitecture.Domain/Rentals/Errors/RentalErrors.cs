using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Errors
{
    public class RentalErrors
    {
        public static Error NotFound = new Error(
            "Rental.NotFound",
            "The rental with the specified id was not found"
        );

        public static Error Overlap = new Error(
            "Rental.Overlap",
            "The rental has been taken by 2 people at the same time"
        );

        public static Error NotReserved = new Error(
            "Rental.NotReserved",
            "The rental it's not reserved"
        );

        public static Error NotConfirmed = new Error(
            "Rental.NotConfirmed",
            "The rental it's not confirmed"
        );

        public static Error AlreadyStarted = new Error(
            "Rental.AlreadyStarted",
            "The rental has already started"
        );
    }
}