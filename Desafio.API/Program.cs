using System.Globalization;
using Desafio.Infra;
using Desafio.Infra.Repository;
using Desafio.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddControllersAsServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



string databasePath = System.IO.Path.Combine(((IWebHostEnvironment)builder.Environment).ContentRootPath, "desafio.db");
var connection = $"Data Source={databasePath}";
builder.Services.AddDbContext<Desafio.Infra.DesafioContext>(options =>
    options.UseSqlite(connection)
);

builder.Services.AddScoped<IUnitOfWork>(s => new DesafioUnitOfWork(s.GetRequiredService<DesafioContext>()));
builder.Services.AddScoped<AccountTransactionRepository>(s => new AccountTransactionRepository(s.GetRequiredService<DesafioContext>()));
builder.Services.AddScoped<AccountTransactionService>(s =>
   new AccountTransactionService(s.GetRequiredService<AccountTransactionRepository>(),
   s.GetRequiredService<IUnitOfWork>()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

//Atualiza o banco de dados
using (var scope = app.Services.CreateScope())
{
   var services = scope.ServiceProvider;

   var context = services.GetRequiredService<DesafioContext>();
   context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
