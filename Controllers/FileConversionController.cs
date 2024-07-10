using FileConvertor.Models;
using FileConvertor.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileConvertor.Controllers
{
    /// <summary>
    /// This controller is responsible for converting files from URL or content to PDF.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/fileconversion")]
    [ApiController]
    public class FileConversionController(IFileConversionService fileConversionService) : ControllerBase
    {
        /// <summary>
        /// Converts the documents from URL.
        /// </summary>
        /// <param name="urlConversionRequests">The URL conversion requests.</param>
        /// <remarks>
        /// This endpoint is used to pass the URL of the documents to be converted to PDF.
        /// </remarks>
        /// <returns>
        /// The list of file conversion responses.
        /// </returns>
        [HttpPost("fileurl")]
        public async Task<IActionResult> ConvertDocumentsFromUrl(IEnumerable<FileConversionRequestDto> urlConversionRequests)
        {
            var fileConversionResponses = await fileConversionService.Convert(urlConversionRequests);
            return Ok(fileConversionResponses);
        }

        /// <summary>
        /// Converts the content of the documents from HTML to PDF.
        /// </summary>
        /// <param name="contentConversionRequests">The content conversion requests.</param>
        /// <remarks>
        /// This endpoint is used to pass the content of the documents to be converted to PDF.
        /// </remarks>
        /// <returns>
        /// This list of file conversion responses.
        /// </returns>
        [HttpPost("filecontent")]
        public async Task<IActionResult> ConvertDocumentsFromContent(IEnumerable<FileConversionContentRequestDto> contentConversionRequests)
        {
            var conversionRequests = this.BuildFileConversionRequests(contentConversionRequests);
            var fileConversionResponses = await fileConversionService.Convert(conversionRequests, ConversionType.FilePath);
            conversionRequests.ForEach(request => System.IO.File.Delete(request.FileData));

            return Ok(fileConversionResponses);
        }

        /// <summary>
        /// Converts the documents from data.
        /// </summary>
        /// <param name="dataConversionRequests">The data conversion requests.</param>
        /// <returns>
        /// The list of file conversion responses.
        /// </returns>
        [HttpPost("filedata")]
        public async Task<IActionResult> ConvertDocumentsFromData(IEnumerable<FileConversionContentRequestDto> dataConversionRequests)
        {
            var conversionRequests = dataConversionRequests.Select(FileConversionRequestDto.CreateNew);
            var fileConversionResponses = await fileConversionService.Convert(conversionRequests, ConversionType.HtmlString);

            return Ok(fileConversionResponses);
        }

        /// <summary>
        /// Builds the file conversion requests.
        /// </summary>
        /// <param name="contentConversionRequests">The content conversion requests.</param>
        /// <returns>
        /// The list of file conversion requests.
        /// </returns>
        private List<FileConversionRequestDto> BuildFileConversionRequests(IEnumerable<FileConversionContentRequestDto> contentConversionRequests)
        {
            var conversionRequests = new List<FileConversionRequestDto>();

            foreach (var conversionRequestDto in contentConversionRequests)
            {
                var conversionRequest = new FileConversionRequestDto()
                {
                    FileIdentifier = conversionRequestDto.FileIdentifier,
                    FileData = Path.GetTempFileName(),
                };
                System.IO.File.WriteAllBytes(conversionRequest.FileData, Convert.FromBase64String(conversionRequestDto.FileContent));
                conversionRequests.Add(conversionRequest);
            }

            return conversionRequests;
        }
    }
}
