using API.Middlewares;
using DataAccess.Data;
using DataAccess.RepositoriesImplementation;
using Core.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using DataAccess.Identity;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddIdentityCore<AppUser>(opt =>
{

}).AddEntityFrameworkStores<AppIdentityDbContext>()
.AddSignInManager<SignInManager<AppUser>>();

var jwt = builder.Configuration.GetSection("Token");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["key"])),
			ValidIssuer = jwt["Issuer"],
			ValidateIssuer = true,
			ValidateAudience = false
		};
	}
	);
builder.Services.AddAuthorization();	

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
var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
	await context.Database.MigrateAsync();
	await identityContext.Database.MigrateAsync();
	await ApplicationContextSeed.SeedAsync(context);
	await AppIdentityDbContextSeed.SeedUserAsync(userManager);
}
catch (Exception ex)
{
	logger.LogError(ex, "Error occured while migrating process");
}

app.Run();

