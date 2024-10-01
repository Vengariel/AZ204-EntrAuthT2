using AZ204_EntraAPI.Services;
using AZ204_EntrAuth;
using AZ204_EntrAuth.Clients;
using AZ204_EntrAuth.HttpClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register services
builder.Services.AddScoped<IGraphHttpClient, GraphHttpClient>();

// TODO: Enable service injection
//builder.Services.AddSingleton<ISettingsProvider, SettingsProvider>();
//builder.Services.AddScoped<IPublicClient, PublicClient>();
//builder.Services.AddScoped<IAuthService, AuthService>();

// add support for web api controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.UseHttpsRedirection();

// MAPPED ENDPOINTS
app.MapGet("/auth", async () =>
{
	ISettingsProvider settingsProvider = new SettingsProvider();
	IPublicClient publicClient = new PublicClient();
	var authService = new AuthService(publicClient, settingsProvider);

	return await authService.GetAccessToken();
})
.WithOpenApi();

app.Run();