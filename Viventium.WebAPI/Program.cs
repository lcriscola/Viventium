using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Viventium.Business;
using Viventium.Repositores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddViventiumBusinessServices();
builder.Services.AddViventiumRepositories(builder.Configuration.GetConnectionString("Viventium")!);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Viventium Test", Version = "v1" });
    var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Viventium.WebAPI.xml");
    x.IncludeXmlComments(filePath, true);

});



//builder.WebHost.ConfigureKestrel((options) =>
//{
//    options.ConfigureEndpointDefaults(lo => lo.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2);
//});

var app = builder.Build();


app.UseCors(x =>
{
    x.WithOrigins("http://localhost:4200");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapControllers();

app.Run();
