using TechMove.Services;

namespace TechMove.Tests
{
    public class FileValidationServiceTests
    {
        private readonly FileValidationService _fileValidationService;

        /* Microsoft 2024
 * Unit testing best practices with .NET
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices>
 * Accessed: 22 April 2026
 */
        /* Microsoft 2024
* Unit testing C# with xUnit
* Microsoft Learn
* <https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test>
* Accessed: 22 April 2026
*/
        public FileValidationServiceTests()
        {
            _fileValidationService = new FileValidationService();
        }

        [Fact]
        public void IsPdf_WithPdfFile_ReturnsTrue()
        {
            bool result = _fileValidationService.IsPdf("agreement.pdf");
            Assert.True(result);
        }

        [Fact]
        public void IsPdf_WithWordFile_ReturnsFalse()
        {
            bool result = _fileValidationService.IsPdf("agreement.docx");
            Assert.False(result);
        }

        [Fact]
        public void IsPdf_WithImageFile_ReturnsFalse()
        {
            bool result = _fileValidationService.IsPdf("photo.jpg");
            Assert.False(result);
        }

        [Fact]
        public void IsWithinSizeLimit_SmallFile_ReturnsTrue()
        {
            long smallFile = 1 * 1024 * 1024; // 1MB
            bool result = _fileValidationService.IsWithinSizeLimit(smallFile);
            Assert.True(result);
        }

        [Fact]
        public void IsWithinSizeLimit_LargeFile_ReturnsFalse()
        {
            long largeFile = 10 * 1024 * 1024; // 10MB - over limit
            bool result = _fileValidationService.IsWithinSizeLimit(largeFile);
            Assert.False(result);
        }

        [Fact]
        public void IsValidFile_ValidPdfUnderLimit_ReturnsTrue()
        {
            long smallFile = 1 * 1024 * 1024; // 1MB
            bool result = _fileValidationService.IsValidFile("contract.pdf", smallFile);
            Assert.True(result);
        }

        [Fact]
        public void IsValidFile_InvalidExtension_ReturnsFalse()
        {
            long smallFile = 1 * 1024 * 1024;
            bool result = _fileValidationService.IsValidFile("contract.exe", smallFile);
            Assert.False(result);
        }
    }
}