using CustomersApi.CasosDeUso;
using CustomersApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddRouting(routing => routing.LowercaseUrls = true); //importante ?
//conexion
builder.Services.AddDbContext<CustomerDatabaseContext>(mysqlBuilder =>
{
    /*aca me traje de appsetting.json como si fuese un .env, la conexion*/
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
});

/*la interfaz y la clase a la que hace referencia*/
builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
