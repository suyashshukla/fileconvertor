using FileConvertor.Models;

namespace FileConvertor.Services
{
    /// <summary>
    /// This interface represents the file conversion service.
    /// </summary>
    public interface IFileConversionService
    {
        /// <summary>
        /// Converts the specified conversion requests.
        /// </summary>
        /// <param name="conversionRequests">The conversion requests.</param>
        /// <param name="conversionType">The conversion type.</param>
        /// <returns>
        /// The list of file conversion response data transfer objects.
        /// </returns>
        Task<IEnumerable<FileConversionResponseDto>> Convert(IEnumerable<FileConversionRequestDto> conversionRequests, ConversionType conversionType = ConversionType.FileUrl);
    }
}
