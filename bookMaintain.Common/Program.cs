using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.GetSection("ConnectionStrings"); 

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
