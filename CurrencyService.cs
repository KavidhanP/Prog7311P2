namespace TechMove.Services
{
    public class CurrencyService
    {
        public decimal ConvertUSDToZAR(decimal usdAmount, decimal exchangeRate)
        {
            if (usdAmount < 0)
                throw new ArgumentException("Amount cannot be negative.");

            if (exchangeRate <= 0)
                throw new ArgumentException("Exchange rate must be greater than zero.");

            return Math.Round(usdAmount * exchangeRate, 2);
        }
    }
}