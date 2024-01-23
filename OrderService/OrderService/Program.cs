using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using OrderService.Context;
using OrderService.MessagingBus;
using OrderService.MessagingBus.RecivedMessage;
using OrderService.MessagingBus.SendMessage;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();  

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MicroserviceOrderDB")),ServiceLifetime.Singleton);
builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("MessageBus").GetSection("RabbitMQ"));
builder.Services.AddHostedService<RecivedOrderCreateMessage>();
builder.Services.AddTransient<IOrderService,OrderService.Services.OrderService>();
builder.Services.AddTransient<IMessageBus, RabbitMQMessageBus>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.Authority = "https://localhost:7096";
    option.Audience = "orderservice";   
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
