namespace TechMove.Services
{
    public class FileValidationService
    {
        private readonly string[] _allowedExtensions = { ".pdf" };
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5MB

        public bool IsValidFile(string fileName, long fileSizeBytes)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return _allowedExtensions.Contains(extension) &&
                   fileSizeBytes <= MaxFileSizeBytes;
        }

        public bool IsPdf(string fileName)
        {
            return Path.GetExtension(fileName).ToLower() == ".pdf";
        }

        public bool IsWithinSizeLimit(long fileSizeBytes)
        {
            return fileSizeBytes <= MaxFileSizeBytes;
        }
    }
}