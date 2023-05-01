using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Desafio.Web;
using Desafio.Infra;
using Desafio.Infra.Repository;
using Desafio.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


string databasePath = System.IO.Path.Combine(
   Directory.GetParent(((IWebHostEnvironment)builder.Environment).ContentRootPath).FullName, "desafio.db");
var connection = $"Data Source={databasePath}";
builder.Services.AddDbContext<Desafio.Infra.DesafioContext>(options =>
    options.UseSqlite(connection)
);

builder.Services.AddScoped<IUnitOfWork>(s => new DesafioUnitOfWork(s.GetRequiredService<DesafioContext>()));
builder.Services.AddScoped<AccountTransactionRepository>(s => new AccountTransactionRepository(s.GetRequiredService<DesafioContext>()));
builder.Services.AddScoped<AccountTransactionService>(s => 
   new AccountTransactionService(s.GetRequiredService<AccountTransactionRepository>(),
   s.GetRequiredService<IUnitOfWork>() ));
   

var app = builder.Build();

//Atualiza o banco de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DesafioContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
