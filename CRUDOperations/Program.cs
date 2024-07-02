global using CRUDOperations.Models;
using CRUDOperations.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using CRUDOperations.Configuration;
using CRUDOperations.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using LlmTornado;
using LlmTornado.Code;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.Configure<OpenAIConfig>(builder.Configuration.GetSection("OpenAI"));
builder.Services.Configure<CohereAIConfig>(builder.Configuration.GetSection("COHERE_KEY"));

// Register CohereService with HttpClient configuration
builder.Services.AddHttpClient<CohereService>(client =>
{
    client.BaseAddress = new Uri("https://api.cohere.com/v1/chat");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    AllowAutoRedirect = false,
    UseCookies = false
});

// builder.Services.AddHttpClient<CohereService>(client =>
//     {
//         client.BaseAddress = new Uri("https://api.cohere.com/v1/chat");
//         // The API key will be set in the service constructor
//     });

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Configure API versioning
// builder.Services.AddApiVersioning(options =>
// {
//     options.DefaultApiVersion = new ApiVersion(1, 0);
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.ReportApiVersions = true;
// });

// // Add Tornado API configuration
// builder.Services.AddSingleton(new TornadoApi(new List<ProviderAuthentication>
// {
//     new ProviderAuthentication(LLmProviders.OpenAi, "OPEN_AI_KEY"),
//     new ProviderAuthentication(LLmProviders.Cohere, "COHERE_KEY")
// }));

// // Configure Swagger generation
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenAI", Version = "v1" });
// });

// Register HttpClient
builder.Services.AddSingleton<HttpClient>();

// builder.Services.AddScoped<IOpenAIService, OpenAiService>();
builder.Services.AddScoped<CohereService>();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "YourAppName.Cookie";
        options.LoginPath = "/Account/Login"; // Adjust as per your application's login path
    });

// Register CohereService and HttpClient
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();

// if(app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
//     // app.UseSwaggerUI(options =>
//     // {
//     //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
//     //     options.RoutePrefix = string.Empty; // Set the Swagger UI at the root URL
//     // });
// }

// Register OpenAiService with API key from appsettings.json
// var configuration = builder.Configuration;
// string openAiApiKey = configuration["OpenAI:ApiKey"];
// if (string.IsNullOrEmpty(openAiApiKey))
// {
//     throw new InvalidOperationException("OpenAI API key is missing or empty in appsettings.json");
// }

// builder.Services.AddSingleton<OpenAiService>();

// Log the API key for verification
// Console.WriteLine($"OpenAI API Key: {openAiApiKey}");

// Register OpenAiService with HttpClient and API key
// builder.Services.AddSingleton<OpenAiService>(sp =>
// new OpenAiService(sp.GetRequiredService<HttpClient>(), openAiApiKey));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();