namespace CleanArchitecture.Domain.Abstractions
{
    public record Error(string Code, string Detail)
    {
        public static Error None = new Error("", "");
        public static Error NullValue = new Error("Error.NullValue", "A null value was passed");
    }
}