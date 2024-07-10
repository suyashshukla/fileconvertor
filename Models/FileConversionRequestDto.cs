namespace FileConvertor.Models
{
    /// <summary>
    /// This class represents the file conversion request data transfer object.
    /// </summary>
    public class FileConversionRequestDto
    {
        /// <summary>
        /// Gets or sets the file data. It can either be URL, file path or raw text.
        /// </summary>
        /// <value>
        /// The file URL.
        /// </value>
        public string FileData { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public Guid FileIdentifier { get; set; }

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="fileConversionContentRequestDto">The file conversion content request dto.</param>
        /// <returns>
        /// The file conversion request dto.
        /// </returns>
        public static FileConversionRequestDto CreateNew(FileConversionContentRequestDto fileConversionContentRequestDto)
        {
            return new FileConversionRequestDto()
            {
                FileIdentifier = fileConversionContentRequestDto.FileIdentifier,
                FileData = fileConversionContentRequestDto.FileContent,
            };
        }
    }
}
