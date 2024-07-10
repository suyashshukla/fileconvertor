namespace FileConvertor.Models
{
    /// <summary>
    /// This class represents the file conversion response data transfer object.
    /// </summary>
    public class FileConversionResponseDto
    {
        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public Guid FileIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the converted document URL.
        /// </summary>
        /// <value>
        /// The converted document URL.
        /// </value>
        public string ConvertedDocumentUrl { get; set; } = string.Empty;

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="fileIdentifier">The file identifier.</param>
        /// <param name="convertedDocumentUrl">The converted document URL.</param>
        /// <returns>
        /// The file conversion response dto.
        /// </returns>
        public static FileConversionResponseDto CreateNew(Guid fileIdentifier, string convertedDocumentUrl)
        {
            return new FileConversionResponseDto
            {
                FileIdentifier = fileIdentifier,
                ConvertedDocumentUrl = convertedDocumentUrl
            };
        }
    }
}
