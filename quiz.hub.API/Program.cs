using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using quiz.hub.API.Extentions;
using quiz.hub.API.Middlewares;
using quiz.hub.Domain.Identity;
using quiz.hub.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);


// database connection service
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o=> 
{
    o.Password.RequireNonAlphanumeric = true;
    o.Password.RequireDigit = true;
    o.Password.RequiredLength = 6;
    o.Password.RequireLowercase = true;
    o.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// use global exception handler middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// seed Identity user and roles
await app.Services.SeedIdentityAsync();

app.Run();
