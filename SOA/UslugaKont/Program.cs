using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<UslugaDBOptions>(
    builder.Configuration.GetSection("UslugaDB")
);

builder.Services.AddHttpClient("UslugaDB", (serviceProvider, httpClient) => {
    var opts = serviceProvider.GetRequiredService<IOptions<UslugaDBOptions>>();

    httpClient.BaseAddress = new Uri(opts.Value.Url);

    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, "SkrzynkaPocztowa");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/konta", async (IHttpClientFactory clientFactory, IOptions<JsonOptions> options) =>
{
    if (clientFactory is null)
        return Results.Problem("Server error", statusCode: 500);

    var httpClient = clientFactory?.CreateClient("UslugaDB");

    var response = await httpClient!.GetAsync("/konta");
    if (response.IsSuccessStatusCode)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();
        var konta = await JsonSerializer.DeserializeAsync<IEnumerable<Konto>>(contentStream, options.Value.SerializerOptions);

        if (konta is null)
            return Results.Problem("Unable to retrieve Konta", statusCode: 500);

        return Results.Ok(konta);
    }
    return Results.Problem("Unable to connect to UslugaKont", statusCode: 500);
});

app.MapPost("/konta", async (Konto konto, IHttpClientFactory clientFactory, IOptions<JsonOptions> options) =>
{
    if (clientFactory is null)
        return Results.Problem("Server error", statusCode: 500);

    var httpClient = clientFactory?.CreateClient("UslugaDB");

    var jsonStream = JsonContent.Create<Konto>(konto, options: options.Value.SerializerOptions);

    var response = await httpClient!.PostAsync("/konta", jsonStream);

    if (response.IsSuccessStatusCode)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();
        var k = await JsonSerializer.DeserializeAsync<Konto>(contentStream, options.Value.SerializerOptions);

        if (k is null)
            return Results.Problem("Unable to retrieve add Konto result", statusCode: 500);

        return Results.Ok(k);
    }

    return Results.Problem("Unable to connect to UslugaKont", statusCode: 500);
});

app.MapDelete("/konta/{id:int}", async (int id, IHttpClientFactory clientFactory) =>
{
    if (clientFactory is null)
        return Results.Problem("Server error", statusCode: 500);

    var httpClient = clientFactory?.CreateClient("UslugaDB");

    var response = await httpClient!.DeleteAsync($"/konta/{id}");
    if (response.IsSuccessStatusCode)
    {
        return Results.NoContent();
    }
    return Results.Problem("Unable to connect to UslugaKont", statusCode: 500);
});


app.Run();
