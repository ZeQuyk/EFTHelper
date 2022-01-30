using System;

namespace EFTHelper.Helpers
{
    public static class CurrencyHelper
    {
        #region Constants

        private const string Roubles = "RUB";
        private const string RoublesSymbol = "₽";
        private const string USDollars = "USD";
        private const string USDollarsSymbol = "$";
        private const string Euro = "EUR";
        private const string EuroSymbol = "€";

        #endregion

        #region Methods

        public static string GetCurrencySymbol(string currency)
        {                                     
            if (string.IsNullOrEmpty(currency) || currency.Equals(Roubles, StringComparison.InvariantCultureIgnoreCase))
            {
                return RoublesSymbol;
            }

            if (currency.Equals(USDollars, StringComparison.InvariantCultureIgnoreCase))
            {
                return USDollarsSymbol;
            }

            if (currency.Equals(Euro, StringComparison.InvariantCultureIgnoreCase))
            {
                return EuroSymbol;
            }

            return string.Empty;
        }

        #endregion
    }
}
