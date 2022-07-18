using Business_access_layer.Services;
using Data_Access_Layer.Data;
using Data_Access_Layer.Repositories;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDbContext<masterContext>(options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<RepositoryPrincipalTask, RepositoryPrincipalTask>();
builder.Services.AddScoped<RepositorySecondaryTask, RepositorySecondaryTask>();

builder.Services.AddScoped<ServiceSecondaryTask,ServiceSecondaryTask>();
builder.Services.AddScoped<ServicePrincipalTask, ServicePrincipalTask>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corspolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
