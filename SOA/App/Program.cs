using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<AppData>();

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


builder.Services.Configure<UslugaWiadomosciOptions>(
    builder.Configuration.GetSection("UslugaWiadomosci")
);

builder.Services.AddHttpClient("UslugaWiadomosci", (serviceProvider, httpClient) => {
    var opts = serviceProvider.GetRequiredService<IOptions<UslugaWiadomosciOptions>>();

    httpClient.BaseAddress = new Uri(opts.Value.Url);

    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, "SkrzynkaPocztowa");
});

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
