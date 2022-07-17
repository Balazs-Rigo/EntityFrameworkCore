using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreMovies.Entities.Conversions
{
    public class CurrencyToSymbolConverter : ValueConverter<Currency, string>
    {
        public CurrencyToSymbolConverter() :
            base(value => MapCurrencyToString(value), 
                value => MapToStringToCurrency(value))
        {

        }

        private static string MapCurrencyToString(Currency value)
        {
            return value switch
            {
                Currency.DominicanPeso => "RD$",
                Currency.USDollar => "$",
                Currency.Euro => "€",
                _ => ""
            };
        }

        private static Currency MapToStringToCurrency(string value)
        {
            return value switch
            {
                "RD$" => Currency.DominicanPeso,
                "$" => Currency.USDollar,
                "€" => Currency.Euro,
                _ => Currency.Unknown
            };
        }
    }
}
