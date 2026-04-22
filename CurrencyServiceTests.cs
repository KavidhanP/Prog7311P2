using TechMove.Services;

namespace TechMove.Tests
{
    public class CurrencyServiceTests
    {
        /* Microsoft 2024
 * Unit testing C# with xUnit
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test>
 * Accessed: 22 April 2026
 */

        /* Microsoft 2024
 * Unit testing best practices with .NET
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices>
 * Accessed: 22 April 2026
 */
        private readonly CurrencyService _currencyService;

        public CurrencyServiceTests()
        {
            _currencyService = new CurrencyService();
        }

        [Fact]
        public void ConvertUSDToZAR_CorrectAmount_ReturnsConvertedValue()
        {
            // Arrange - set up the inputs
            decimal usdAmount = 100;
            decimal exchangeRate = 16.31m;

            // Act - run the method
            decimal result = _currencyService.ConvertUSDToZAR(usdAmount, exchangeRate);

            // Assert - check the result
            Assert.Equal(1631.00m, result);
        }

        [Fact]
        public void ConvertUSDToZAR_ZeroAmount_ReturnsZero()
        {
            decimal result = _currencyService.ConvertUSDToZAR(0, 16.31m);
            Assert.Equal(0m, result);
        }

        [Fact]
        public void ConvertUSDToZAR_NegativeAmount_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                _currencyService.ConvertUSDToZAR(-100, 16.31m));
        }

        [Fact]
        public void ConvertUSDToZAR_InvalidRate_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                _currencyService.ConvertUSDToZAR(100, 0));
        }

        [Fact]
        public void ConvertUSDToZAR_RoundsToTwoDecimalPlaces()
        {
            decimal result = _currencyService.ConvertUSDToZAR(1, 16.319m);
            Assert.Equal(16.32m, result);
        }
    }
}