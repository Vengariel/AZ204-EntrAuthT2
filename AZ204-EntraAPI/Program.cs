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
builder.Services.AddSingleton<ISettingsProvider, SettingsProvider>();
builder.Services.AddScoped<IPublicClient, PublicClient>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMsGraphService, MsGraphService>();
builder.Services.AddSingleton((c) => { 
	return new SettingsProvider().GetAppSettings().Get<AppSettings>() ?? new AppSettings();
});
builder.Services.AddSingleton(c => { 
	var settings = c.GetRequiredService<AppSettings>().AzureSettings;

	return settings.ConfidentialClient;
});

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

app.Run();