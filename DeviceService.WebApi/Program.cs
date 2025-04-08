using CommonLib.EFCore.Extensions;
using DeviceService.Data;
using DeviceService.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddPostgresDbContext<DeviceServiceDbContext>(builder.Configuration);
builder.Services.AddMappers();
builder.Services.AddCommon();
builder.Services.AddServices();

var app = builder.Build();
await app.Services.ApplyMigrationAsync<DeviceServiceDbContext>();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.Run();