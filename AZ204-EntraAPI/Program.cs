using AZ204_EntraAPI.Services;
using AZ204_EntrAuth;
using AZ204_EntrAuth.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Enable service injection
// register services
//builder.Services.AddSingleton<ISettingsProvider, SettingsProvider>();
//builder.Services.AddScoped<IPublicClient, PublicClient>();
//builder.Services.AddScoped<IAuthService, AuthService>();

// TODO: add support for web api controllers
// add support for web api controllers
//builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/auth", async () =>
{
	ISettingsProvider settingsProvider = new SettingsProvider();
	IPublicClient publicClient = new PublicClient();
	var authService = new AuthService(publicClient, settingsProvider);

	return await authService.GetAccessToken();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
