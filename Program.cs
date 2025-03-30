using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Repositories;
using RestaurantPOS.Services;
using System.Text.Json.Serialization;
using RestaurantPOS.Middlewares;
using RestaurantPOS.Mappings;

var builder = WebApplication.CreateBuilder(args);


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core in-memory
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase("RestaurantPOS")
);

// DI
//? Controllers
builder.Services.AddControllers()
        .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


//? Services
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<CategoryService>();

//? Repositories
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//? AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();
