using Microsoft.EntityFrameworkCore;
using ProductService.Context;
using ProductService.Repositories.BaseRepository;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add dbcontext
builder.Services.AddDbContext<ProductContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MicroserviceProductDB")));

#region ServiceRegister
builder.Services.AddTransient<IProductService, ProductService.Services.ProductService>();
builder.Services.AddTransient<ICategoryService, ProductService.Services.CategoryService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
