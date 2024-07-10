namespace FileConvertor.Models
{
    /// <summary>
    /// This class represents the constants.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The file conversion container name
        /// </summary>
        public const string FileConversionContainerName = "file-conversions";

        /// <summary>
        /// The sync fusion license key
        /// </summary>
        public const string SyncFusionLicenseKey = "LICENSE";

        /// <summary>
        /// Gets the name of the file conversion BLOB.
        /// </summary>
        /// <param name="fileIdentifier">The file identifier.</param>
        /// <returns>
        /// The file conversion blob name.
        /// </returns>
        public static string GetFileConversionBlobName(Guid fileIdentifier)
        {
            return $"{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}/{DateTime.UtcNow.Day}/{fileIdentifier}.pdf";
        }
    }
}
