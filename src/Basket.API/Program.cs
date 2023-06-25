using Basket.API.Repostories;
using Basket.API.Services;
using Discount.GRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCacheConnection"];
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// Registering GRPC related services starts here
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(option =>
{
    option.Address = new Uri(builder.Configuration.GetSection("GrpcSettings:GrpcAddress").Value);
});
builder.Services.AddScoped<DiscountGRPCService>();
// Registering GRPC related services ends here


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
