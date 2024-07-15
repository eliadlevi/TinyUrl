using TinyUrl.Cache;
using TinyUrl.DB;
using TinyUrl.Middleware;
using TinyUrl.Models;
using TinyUrl.UrlShortBL.Checksum;
using TinyUrl.UrlShortBL.CreateShortUrl;
using TinyUrl.UrlShortBL.UrlGetter;
using TinyUrl.UrlShortBL.UrlShortning;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddConsole();

builder.Services.AddExceptionHandler<AppExceptionHandler>();

// Add the db connection settings to the configuration
builder.Services.Configure<UrlShortsDatabaseSettings>(
    builder.Configuration.GetSection("UrlShortsDatabase"));
builder.Services.AddSingleton<IChecksum, MD5Checksum>();
builder.Services.AddSingleton<IShortUrl, ShortUrlCheckSum>();
builder.Services.AddSingleton<IUrlDbService, UrlsMongoService>();
builder.Services.AddSingleton<IShortUrlCreator, ShortUrlCreator>();
builder.Services.AddSingleton<ICache<string, Url>>(new Cache<string, Url>(2));
builder.Services.AddSingleton<IOriginalUrlGetter, OriginalUrlGetter>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler(_ => { });
app.UseAuthorization();

app.MapControllers();
app.Logger.LogInformation("Starting the app");
app.Run();
