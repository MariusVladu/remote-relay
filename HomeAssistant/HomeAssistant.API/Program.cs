using HomeAssistant.BusinessLogic.Contracts;
using HomeAssistant.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();


app.MapGet("/relays", async (IRelaysBusinessLogic businessLogic) => await businessLogic.GetAll());
app.MapPut("/relays/{id}/switch/{k}/on", async (string id, int k, IRelaysBusinessLogic businessLogic) => await businessLogic.SwitchOn(id, k));
app.MapPut("/relays/{id}/switch/{k}/off", async (string id, int k, IRelaysBusinessLogic businessLogic) => await businessLogic.SwitchOff(id, k));

app.Run();
