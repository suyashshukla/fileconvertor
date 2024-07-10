using Azure.Storage.Blobs;
using FileConvertor.Models;
using FileConvertor.Services;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Constants.SyncFusionLicenseKey);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFileConversionService, FileConversionService>();
builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();
builder.Services.AddScoped(_ => new BlobServiceClient(builder.Configuration["StorageAccount"]));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
