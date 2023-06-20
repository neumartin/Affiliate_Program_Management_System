using APMS.DataAccess.Context;
using APMS.Managers.Common;
using APMS.Managers.Interfaces;
using APMS.Managers.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApmsDbContext>();

builder.Services.AddSingleton<IApiKeyManager, ApiKeyManager>();
builder.Services.AddSingleton<IAffiliateManager, AffiliateManager>();
builder.Services.AddSingleton<ICustomerManager, CustomerManager>();

DataManager dataManager = new DataManager();
dataManager.Initialize();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
