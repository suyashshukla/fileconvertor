namespace FileConvertor.Models
{
    /// <summary>
    /// This class represents the file conversion content request data transfer object.
    /// </summary>
    public class FileConversionContentRequestDto
    {
        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
        /// <value>
        /// The content of the file.
        /// </value>
        public string FileContent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public Guid FileIdentifier { get; set; }
    }
}
