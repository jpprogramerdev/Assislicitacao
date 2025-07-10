using Assislicitacao.Context;
using Assislicitacao.DAO;
using Assislicitacao.DAO.Interface;
using Assislicitacao.Facade;
using Assislicitacao.Facade.Interface;
using Assislicitacao.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IDAOTipoUsuario, DAOTiposUsuario>();
builder.Services.AddTransient<IDAOUsuario, DAOUsuario>();
builder.Services.AddTransient<IDAOEstado, DAOEstado>();
builder.Services.AddTransient<IDAOMunicipio, DAOMunicipio>();
builder.Services.AddTransient<IDAOPorteEmpresa, DAOPorteEmpresa>();
builder.Services.AddTransient<IDAOEndereco, DAOEndereco>();
builder.Services.AddTransient<IDAOEmpresa, DAOEmpresa>();


builder.Services.AddScoped<IFacadeTipoUsuario, FacadeTipoUsuario>();
builder.Services.AddScoped<IFacadeUsuario, FacadeUsuario>();
builder.Services.AddScoped<IFacadeEmpresa, FacadeEmpresa>();

builder.Services.AddHttpClient<ReceitaWsService>();


var app = builder.Build();

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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=RegistrarUsuario}/{id?}");

app.Run();
