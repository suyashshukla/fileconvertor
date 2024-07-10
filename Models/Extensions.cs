using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FileConvertor.Models
{
    /// <summary>
    /// This class represents the extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Generates the sa s URL.
        /// </summary>
        /// <param name="blobClient">The BLOB client.</param>
        /// <returns></returns>
        public static string GenerateSaSUrl(this BlobClient blobClient)
        {
            return blobClient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read, DateTimeOffset.Now.AddHours(1)).AbsoluteUri;
        }

        /// <summary>
        /// Uploads the file asynchronous.
        /// </summary>
        /// <param name="blobClient">The BLOB client.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="stream">The file content stream.</param>
        /// <returns>The file url for </returns>
        public static async Task<string> UploadFileAsync(this BlobClient blobClient, string filePath, Stream stream)
        {
            var blobHeaders = new BlobHttpHeaders
            {
                ContentType = filePath.GetContentType()
            };
            await blobClient.UploadAsync(stream, blobHeaders);
            return blobClient.GenerateSaSUrl();
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The content type for the specified file name.</returns>
        public static string GetContentType(this string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".txt":
                    return "text/plain";
                case ".pdf":
                    return "application/pdf";
                case ".csi":
                case ".zip":
                default:
                    return "application/octet-stream";
            }
        }
    }
}
