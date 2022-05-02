var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Repository>();

builder.Services.AddCors(cors =>
{
    cors.AddPolicy("DBCorsPolicy", options => options.WithOrigins("http://localhost:5001"));
});

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:5001"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetService<Repository>();
    if (repository is null)
    {
        throw new Exception("Repository is not loaded.");
    }
    repository.Seed();
}

app.MapGet("/getemails", (Repository repo) =>
{
    return repo.Mails.ToArray();
})
.WithName("GetEmails");

app.MapPost("/clearemails", (Repository repo) =>
{
    repo.Clear();
    return Results.NoContent();
})
.WithName("ClearEmails")
.Produces(204);

app.MapPost("/createemail", (Email email, Repository repo) =>
{
    repo.AddEmail(email);
    return Results.Ok();
})
.WithName("CreateEmail")
.Produces(200);

app.Run("http://localhost:7051");
