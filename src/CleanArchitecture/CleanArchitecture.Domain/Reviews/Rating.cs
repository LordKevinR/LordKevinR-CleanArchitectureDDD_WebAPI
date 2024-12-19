using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews
{
    public sealed record Rating
    {
        public static readonly Error Invalid = new Error("Rating.Invalid", "The rating is invalid");

        public int Value { get; init; }

        private Rating(int value) => Value = value;

        public static Result<Rating> Create(int Value)
        {
            if (Value < 1 || Value > 5)
                return Result.Failure<Rating>(Invalid);

            return new Rating(Value);
        }
    }
}