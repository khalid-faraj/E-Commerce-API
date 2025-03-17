using API.Middlewares;
using DataAccess.Data;
using DataAccess.RepositoriesImplementation;
using Core.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(opt =>
{
	opt.AddPolicy("CorsPolicy", policy =>
	{
		policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
	});
}
);

builder.Services.AddSingleton<IConnectionMultiplexer>( c=>
{
	var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
	return ConnectionMultiplexer.Connect(options);
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseRouting();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ApplicationContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
	await context.Database.MigrateAsync();
	await ApplicationContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
	logger.LogError(ex, "Error occured while migrating process");
}

app.Run();

