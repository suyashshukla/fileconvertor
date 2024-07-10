using FileConvertor.Models;
using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.Web;

namespace FileConvertor.Builders
{
    public class SyncfusionDocumentBuilder
    {
        /// <summary>
        /// Builds the converted document stream.
        /// </summary>
        /// <param name="conversionRequest">The conversion request.</param>
        /// <param name="conversionType">Type of the conversion.</param>
        /// <returns>
        /// The memory stream with the converted document.
        /// </returns>
        public static MemoryStream BuildConvertedDocumentStream(FileConversionRequestDto conversionRequest, ConversionType conversionType = ConversionType.FileUrl)
        {
            var htmlToPdfConverter = BuildHtmlConvertor();
            //Convert HTML string to PDF
            PdfDocument document;

            switch (conversionType)
            {
                case ConversionType.FileUrl:
                    document = htmlToPdfConverter.Convert(conversionRequest.FileData);
                    break;
                case ConversionType.FilePath:
                    document = htmlToPdfConverter.Convert(File.ReadAllText(conversionRequest.FileData), Path.GetTempPath());
                    break;
                case ConversionType.HtmlString:
                default:
                    var htmlString = HttpUtility.HtmlDecode(conversionRequest.FileData);
                    document = htmlToPdfConverter.Convert(htmlString, Path.GetTempPath());
                    break;
            }

            //Save the document into stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            //Close the document
            document.Close(true);

            return stream;
        }

        /// <summary>
        /// Builds the HTML convertor.
        /// </summary>
        /// <returns>
        /// The html to pdf convertor instance.
        /// </returns>
        private static HtmlToPdfConverter BuildHtmlConvertor()
        {
            //Initialize HTML to PDF converter 
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            BlinkConverterSettings settings = new BlinkConverterSettings();
            //Set command line arguments to run without sandbox.
            settings.CommandLineArguments.Add("--no-sandbox");
            settings.CommandLineArguments.Add("--disable-setuid-sandbox");
            settings.ViewPortSize = new Size(1024, 0);
            //Assign BlinkConverter settings to the HTML converter 
            htmlConverter.ConverterSettings = settings;

            return htmlConverter;
        }
    }
}
