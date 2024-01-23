using Microsoft.EntityFrameworkCore;
using PeymentService.Application.Interfaces;
using PeymentService.Persistence.Database.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<PaymentContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("PaymentDB")));
builder.Services.AddTransient<IDbContextRepository, PaymentContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
