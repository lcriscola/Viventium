using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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


builder.Services.AddDbContext<Viventium.Repositores.ViventiumDataContext>(b =>
{
    b.UseSqlServer(builder.Configuration.GetConnectionString("Viventium")); //110 so openjson is not used  for in(1,2,3) cases
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
