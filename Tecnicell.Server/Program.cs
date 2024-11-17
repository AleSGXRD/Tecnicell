using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Tecnicell.Server.Context;
using Tecnicell.Server.Logic;
using Tecnicell.Server.Models.Common;
using Tecnicell.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*var connectString = builder.Configuration.GetConnectionString("Connect-Pg");
builder.Services.AddDbContext<TecnicellContext>(options => options.UseNpgsql(connectString));*/

var connectString = builder.Configuration.GetConnectionString("Connect-Sqlite");
builder.Services.AddDbContext<TecnicellContext>(options => options.UseSqlite(connectString));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors();
builder.Services.AddControllers();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

//jwt
var appSettings = appSettingsSection.Get<AppSettings>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(opt =>
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

        opt.RequireHttpsMetadata = false;

        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = signingKey,
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(options =>
{
    options.WithOrigins("*");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});
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

app.MapFallbackToFile("/index.html");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TecnicellContext>();
    DbInitializer.Initialize(context);
}
// Function to get the local IP address
string GetLocalIPAddress() {
    var host = Dns.GetHostEntry(Dns.GetHostName());
    foreach (var ip in host.AddressList) {
        if (ip.AddressFamily == AddressFamily.InterNetwork) {
            return ip.ToString(); 
        } 
    } 
    throw new Exception("No network adapters with an IPv4 address in the system!"); 
} 
// Get the IP address and set the URL for the server
string localIpAddress = GetLocalIPAddress(); 
app.Run();

//app.Run();
