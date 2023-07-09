using System.Text.Json;
using EmptyApi.Workflows;
using Microsoft.AspNetCore.Http.Json;
using Something.Application.Example.SomeModel;
using Something.Application.Example.ValidationThoughts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.AddTransient<PetRepository>();
builder.Services.AddTransient<KennelRepository>();
builder.Services.AddTransient<StayRepository>();
builder.Services.AddTransient<StayWorkflow>();

var app = builder.Build();

IResult Process<TIn, TOut>(TIn dto, Func<TIn, Result<TOut>> action)
{
    try
    {
        var r = action(dto);
        if (r.IsValid)
        {
            return Results.Created("/", r.Value);
        }

        return Results.BadRequest(r.Error);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.Problem("Server Error");
    }
}

app.MapGet("/", () => "Hello World!");
app.MapPost("", (CreateStayDTO dto, StayWorkflow wf) => Process(dto, wf.Create));
app.Run();

