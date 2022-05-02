using Microsoft.EntityFrameworkCore;
using ProAgro.Abstractions;
using ProAgro.DAL;
using ProAgro.LogicDomain;
using Microsoft.Extensions.Configuration;
using Nest;
using ProAgro.Repository;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration;
// Add services to the container.
builder.Services.AddTransient<IGeorreferences, GeorreferencesOperations>();
builder.Services.AddTransient<IUsers, UserOperations>();
builder.Services.AddTransient<ProAgro.Abstractions.IRepository<DatabaseContext>, Repository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conf = builder.Configuration;
builder.Services.AddMemoryCache();
builder.Services.AddDbContextFactory<DatabaseContext>(options =>
                options.UseSqlServer(conf.GetValue<string>("Connectionstring"), x => { x.EnableRetryOnFailure(); x.CommandTimeout(120); }));
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

