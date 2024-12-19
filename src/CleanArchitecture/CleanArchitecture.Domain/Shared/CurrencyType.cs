namespace CleanArchitecture.Domain.Shared
{
    public record CurrencyType
    {
        public static readonly CurrencyType None = new("");
        public static readonly CurrencyType USD = new("USD");
        public static readonly CurrencyType EUR = new("EUR");
        public static readonly CurrencyType DOP = new("DOP");

        private CurrencyType(string code) => Code = code;

        public String? Code { get; init; }

        public static readonly IReadOnlyCollection<CurrencyType> All = new[]{
            USD, EUR, DOP
        };

        public static CurrencyType FromCode(string code)
        {
            return All.FirstOrDefault(x => x.Code == code) ?? throw new ApplicationException($"Invalid currency code: {code}");
        }
    }
}