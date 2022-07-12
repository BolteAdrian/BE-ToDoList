using Business_access_layer.Services;
using Data_Access_Layer.Data;
using Data_Access_Layer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<masterContext>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
