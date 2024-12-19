namespace CleanArchitecture.Domain.Shared
{
    public record Currency(CurrencyType CurrencyType, decimal Amount)
    {
        public static Currency operator +(Currency first, Currency second)
        {
            if (first == null || second == null) throw new InvalidOperationException("Currency can't be null");
            if (first.CurrencyType != second.CurrencyType) throw new InvalidOperationException("Currency types don't match");

            return new Currency(first.CurrencyType, first.Amount + second.Amount);
        }

        public static Currency Zero() => new(CurrencyType.None, 0);
        public static Currency Zero(CurrencyType currencyType) => new(currencyType, 0);
        public bool IsZero() => this == Zero(CurrencyType);
    }
}