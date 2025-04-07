using CommonLib.EFCore.Extensions;
using DeviceService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPostgresDbContext<DeviceServiceDbContext>(builder.Configuration);

var app = builder.Build();
await app.Services.ApplyMigrationAsync<DeviceServiceDbContext>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.Run();