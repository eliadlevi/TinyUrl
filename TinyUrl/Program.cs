using TinyUrl.DB;
using TinyUrl.UrlShortBL;

var builder = WebApplication.CreateBuilder(args);

// Add the db connection settings to the configuration
builder.Services.Configure<UrlShortsDatabaseSettings>(
    builder.Configuration.GetSection("UrlShortsDatabase"));
builder.Services.AddSingleton<IChecksum, MD5Checksum>();
builder.Services.AddSingleton<IShortUrl, ShortUrlCheckSum>();
builder.Services.AddSingleton<IUrlDbService, UrlsMongoService>();
builder.Services.AddSingleton<IUrlShortning, UrlShortning>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
