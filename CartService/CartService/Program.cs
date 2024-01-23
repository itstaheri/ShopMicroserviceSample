using CartService.Context;
using CartService.Mapping;
using CartService.MessageBus;
using CartService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add dbcontext
builder.Services.AddDbContext<CartContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MicroserviceCartDB")));

#region RegistortServices
builder.Services.AddAutoMapper(typeof(CartMappingProfile));
builder.Services.AddTransient<ICartService, CartService.Services.CartService>();
builder.Services.AddScoped<IMessageBus, RabbitMQMessageBus>();
builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("MessageBus").GetSection("RabbitMQ"));

#endregion


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
