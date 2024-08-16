using Microsoft.OpenApi.Models;

using Viventium.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddViventiumBusinessServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Viventium Test", Version = "v1" });
    var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Viventium.WebAPI.xml");
    x.IncludeXmlComments(filePath, true);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
