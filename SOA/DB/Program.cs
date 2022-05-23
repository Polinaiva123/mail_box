var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<Context>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/konta", (Context ctx) =>
{
    return ctx.Konta.Records;
});

app.MapGet("/emails", (Context ctx) =>
{
    return ctx.Emails.Records;
});

app.MapPost("/konta", (Konto konto, Context ctx) =>
{
    try
    {
        var result = ctx.Konta.Add(konto);

        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("/emails", (Email email, Context ctx) =>
{
    try
    {
        var result = ctx.Emails.Add(email);

        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("/konta/{id:int}", (int id, Context ctx) =>
{
    try
    {
        ctx.Konta.Usun(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
});

app.MapDelete("/emails/{id:int}", (int id, Context ctx) =>
{
    try
    {
        ctx.Emails.Usun(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
});

app.Run();