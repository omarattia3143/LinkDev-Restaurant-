using System.Text;
using LinkDev.EgyptianRecipes.Auth;
using LinkDev.EgyptianRecipes.Data;
using LinkDev.EgyptianRecipes.Repositories;
using LinkDev.EgyptianRecipes.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType(typeof(TimeSpan), () => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
});

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

}).AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
}));

// Adding Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

// Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });


builder.Services.AddScoped<IBranchRepo, BranchRepo>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IBookingService, BookingService>();






var app = builder.Build();


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    IdentityDbInitializer.SeedUsers(userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseCors("MyPolicy");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();