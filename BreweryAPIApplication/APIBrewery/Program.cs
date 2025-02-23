using BreweryAPIClassLibrary.DataAccess;


var builder = WebApplication.CreateBuilder(args);

// Add Configuration Support
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Register services (Scoped for better DB connection handling)
builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<IBrewerData, BrewerData>();
builder.Services.AddScoped<IWholesalerData, WholesalerData>();

// Add Razor Pages & Controllers
builder.Services.AddControllers();
builder.Services.AddRazorPages();  // ? Added Razor Pages Support

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map Controllers & Razor Pages
app.MapControllers();
app.MapRazorPages();  // ? Enables Razor Pages

app.Run();
