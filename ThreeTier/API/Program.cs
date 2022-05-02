using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add http client (to connect to a repository)
builder.Services.AddHttpClient("DB", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:7051");

    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, "SkrzynkaPocztowa");
});

// Use CORS for connections
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("ApiCorsPolicy", options => options.WithOrigins("http://localhost:3001"));
});

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:3001"));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/emails", async (IHttpClientFactory clientFactory, IOptions<JsonOptions> options) =>
{
    if (clientFactory is null)
    {
        return Results.Problem("Server error", statusCode: 500);
    }

    var httpClient = clientFactory?.CreateClient("DB");

    var response = await httpClient.GetAsync("/getemails");
    if (response.IsSuccessStatusCode)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();
        var emails = await JsonSerializer.DeserializeAsync<IEnumerable<Email>>(contentStream, options.Value.SerializerOptions);

        if (emails is not null)
        {
            if (emails.Any())
            {
                return Results.Ok(emails);
            }
            else
            {
                return Results.NoContent();
            }
        }
        return Results.Problem("Unable to load data from repository.", statusCode: 500);
    }
    return Results.Problem("Unable to load data from repository.", statusCode: 500);
})
.WithName("GetEmails")
.Produces(200)
.Produces(204)
.ProducesProblem(500);

app.MapPost("/email/new", async (Email email, IHttpClientFactory clientFactory, IOptions<JsonOptions> options) =>
{
    if (clientFactory is null)
    {
        return Results.Problem("Server error", statusCode: 500);
    }

    var httpClient = clientFactory?.CreateClient("DB");

    var jsonStream = JsonContent.Create<Email>(email);

    var response = await httpClient.PostAsync("/createemail", jsonStream);

    if (response.IsSuccessStatusCode)
    {
        return Results.Ok();
    }
    return Results.Problem("Unable to load data from repository.", statusCode: 500);
})
.WithName("NewEmails")
.Produces(200)
.ProducesProblem(500);

app.MapPost("/clear", async(IHttpClientFactory clientFactory) =>
{
    if (clientFactory is null)
    {
        return Results.Problem("Server error", statusCode: 500);
    }

    var httpClient = clientFactory?.CreateClient("DB");

    var response = await httpClient.PostAsync("/clearemails", null);

    if (response.IsSuccessStatusCode)
    {
        return Results.NoContent();
    }
    return Results.Problem("Unable to load data from repository.", statusCode: 500);
})
.WithName("ClearEmails")
.Produces(204)
.ProducesProblem(500);

//app.Run();

app.Run("http://localhost:5001");
