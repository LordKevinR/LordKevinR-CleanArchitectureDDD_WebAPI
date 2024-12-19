using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews
{
    public static class ReviewErrors
    {
        public static readonly Error NotElegible = new Error
        ("Review.NotElegible",
        "The review for this vehicle is not elegible because the rental is not completed");
    }
}