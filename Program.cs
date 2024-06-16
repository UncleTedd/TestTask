using AlifTestTask.DbContext;
using AlifTestTask.Helper;
using AlifTestTask.RepositoryService;
using AlifTestTask.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepositoryService>();
builder.Services.AddScoped<VerificationService>();
builder.Services.AddScoped<Functions>();

builder.Services.AddDbContext<AlifDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

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