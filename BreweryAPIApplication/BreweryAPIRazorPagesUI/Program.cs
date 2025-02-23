using BreweryAPIClassLibrary.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Load secrets.json configuration
builder.Configuration.AddUserSecrets<Program>();

// Register Configuration Service
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Register Database Services
builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<IBrewerData, BrewerData>();
builder.Services.AddScoped<IWholesalerData, WholesalerData>();

builder.Services.AddRazorPages();
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // This will log to the console

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
