using LinkDev.EgyptianRecipes.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestaurantContext>(o =>
{
    
    o.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDbString"));

});

//different context so I could separate Identity database from the business database
builder.Services.AddDbContext<RestaurantContext>(o =>
{
    
    o.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDbString"));

});


builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("IdentityDbString")));


builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
{
    //just to simplify things
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.User.RequireUniqueEmail = false;

}).AddEntityFrameworkStores<IdentityContext>();



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