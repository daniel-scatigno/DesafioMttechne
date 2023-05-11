using System.Globalization;
using System.Text;
using Desafio.Infra;
using Desafio.Infra.Repository;
using Desafio.Services;
using Desafio.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddControllersAsServices();

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

//Validators usign FluentValidation
builder.Services.AddScoped<IValidator<AccountTransactionViewModel>, AccountTransactionViewModelValidation>();
builder.Services.AddFluentValidationAutoValidation();

//Authentication
builder.Services.AddAuthentication(options =>
  {
     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
  {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("JWT").GetValue<string>("ValidAudience")!,
        ValidIssuer = builder.Configuration.GetSection("JWT").GetValue<string>("ValidIssuer")!,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT").GetValue<string>("Secret")!))
     };
  });

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();


//Swagger 
app.UseSwagger();
app.UseSwaggerUI();

//Update the Database using EntityFramework
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
