using HomeAssistant.BusinessLogic.Contracts.Ports;
using HomeAssistant.DI;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();


app.MapGet("/relays", async ([FromServices] IRelaysService relaysService) =>
{
    return await relaysService.GetAll();
});

app.Run();
