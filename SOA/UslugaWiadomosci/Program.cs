using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<UslugaKontOptions>(
    builder.Configuration.GetSection("UslugaKont")
);

builder.Services.AddHttpClient("UslugaKont", (serviceProvider, httpClient) => {
    var opts = serviceProvider.GetRequiredService<IOptions<UslugaKontOptions>>();

    httpClient.BaseAddress = new Uri(opts.Value.Url);

    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, "SkrzynkaPocztowa");
});

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

app.MapGet("/emails/{id:int}", async (int id, IHttpClientFactory clientFactory, IOptions<JsonOptions> options) =>
{
    if (clientFactory is null)
        return Results.Problem("Server error", statusCode: 500);

    var httpClient = clientFactory?.CreateClient("UslugaDB");

    var response = await httpClient!.GetAsync("/emails");
    if (response.IsSuccessStatusCode)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();
        var emails = await JsonSerializer.DeserializeAsync<IEnumerable<Email>>(contentStream, options.Value.SerializerOptions);

        if (emails is null)
            return Results.Problem("Unable to retrieve Emails", statusCode: 500);

        var result = emails.Where(e => e.Id == id).FirstOrDefault();

        if (result is null)
            return Results.NotFound();

        return Results.Ok(result);
    }
    return Results.Problem("Unable to connect to UslugaKont", statusCode: 500);
});

app.MapGet("/emails", async ([FromBody] Konto konto, IHttpClientFactory clientFactory, IOptions<JsonOptions> options) =>
{
    if (clientFactory is null)
        return Results.Problem("Server error", statusCode: 500);

    var httpClient = clientFactory?.CreateClient("UslugaDB");

    var response = await httpClient!.GetAsync("/emails");
    if (response.IsSuccessStatusCode)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();
        var emails = await JsonSerializer.DeserializeAsync<IEnumerable<Email>>(contentStream, options.Value.SerializerOptions);

        if (emails is null)
            return Results.Problem("Unable to retrieve Emails", statusCode: 500);

        var result = emails.Where(e => e.To.Id == konto.Id || e.From.Id == konto.Id);

        return Results.Ok(result);
    }
    return Results.Problem("Unable to connect to UslugaDB", statusCode: 500);
});

async Task<Konto?> FindKonto(Konto konto, IHttpClientFactory clientFactory, IOptions<JsonOptions> options)
{
    if (clientFactory is null)
        throw new ApplicationException("Wystąpił błąd aplikacji");

    var httpClient = clientFactory?.CreateClient("UslugaKont");

    var response = await httpClient!.GetAsync("/konta");
    if (response.IsSuccessStatusCode)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();
        var konta = await JsonSerializer.DeserializeAsync<IEnumerable<Konto>>(contentStream, options.Value.SerializerOptions);

        if (konta is null)
            throw new ApplicationException("Nie udało się pobrać dane kont");

        var k = konta.Where(k => k.AdresEmail.Equals(konto.AdresEmail)).FirstOrDefault();

        return k;
    }

    throw new ApplicationException("Nie udało się nawiązać połączenie z usługą");
}

app.MapPost("/emails", async (Email email, IHttpClientFactory clientFactory, IOptions<JsonOptions> options) =>
{
    if (clientFactory is null)
        return Results.Problem("Server error", statusCode: 500);

    var httpClient = clientFactory?.CreateClient("UslugaDB");

    var to = await FindKonto(email.To, clientFactory!, options);

    if (to is null)
        email.To = new Konto { AdresEmail = email.To.AdresEmail, Name = email.To.AdresEmail };
    else
        email.To = to;

    var jsonStream = JsonContent.Create<Email>(email, options: options.Value.SerializerOptions);

    var response = await httpClient!.PostAsync("/emails", jsonStream);

    if (response.IsSuccessStatusCode)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();
        var e = await JsonSerializer.DeserializeAsync<Email>(contentStream, options.Value.SerializerOptions);

        if (e is null)
            return Results.Problem("Unable to retrieve add Email result", statusCode: 500);

        return Results.Ok(e);
    }

    return Results.Problem("Unable to connect to UslugaDB", statusCode: 500);
});

app.MapDelete("/emails/{id:int}", async (int id, IHttpClientFactory clientFactory) =>
{
    if (clientFactory is null)
        return Results.Problem("Server error", statusCode: 500);

    var httpClient = clientFactory?.CreateClient("UslugaDB");

    var response = await httpClient!.DeleteAsync($"/emails/{id}");
    if (response.IsSuccessStatusCode)
    {
        return Results.NoContent();
    }

    return Results.Problem("Unable to connect to UslugaDB", statusCode: 500);
});

app.Run();