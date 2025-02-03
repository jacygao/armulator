using Emu.Controllers.Compute.GalleryController;
using Emu.Controllers.Compute.ImageController;
using Emu.Controllers.Compute.VirtualMachineController;
using Emu.Middlewares;
using Emu.Services.Common;
using Emu.Services.Gallery;
using Emu.Services.Image;
using Emu.Services.VirtualMachine;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();

// Add Azure Storage
// Bind the AzureStorage section in appsettings.json to the AzureStorageOptions class
builder.Services.Configure<AzureStorageOptions>(builder.Configuration.GetSection("AzureStorage"));

// Add AzureBlobStorageService to the DI container
builder.Services.AddSingleton<IStorageService>(provider =>
{
    var storageSettings = provider.GetRequiredService<IOptions<AzureStorageOptions>>();
    if (storageSettings.Value.IsHeadless)
    {
        return new InMemoryStorageService();
    }
    else
    {
        return new AzureBlobStorageService(storageSettings);
    }
});

// Add Domain Services
builder.Services.AddSingleton<IGalleryService, GalleryService>();
builder.Services.AddSingleton<IGalleryImageService, GalleryImageService>();
builder.Services.AddSingleton<IGalleryImageVersionService, GalleryImageVersionService>();
builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<IVirtualMachineService, VirtualMachineService>();

builder.Services.AddScoped<GalleryController.IGalleriesController, GalleryHandler>();
builder.Services.AddScoped<GalleryController.IGalleryImagesController, GalleryImageHandler>();
builder.Services.AddScoped<GalleryController.IGalleryImageVersionsController, GalleryImageVersionHandler>();

builder.Services.AddScoped<ImageController.IImagesController, ImageHandler>();
builder.Services.AddScoped<VirtualMachineController.IVirtualMachinesController, VirtualMachineHandler>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Required for supporting enum conversion
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapHealthChecks("/healthz");

// Add Custom Middlewares
app.UseMiddleware<CommonExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
