using HHBooks.Web.Components;
using HHBooks.Web.Data;
using HHBooks.Web.Interfaces;
using HHBooks.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<HHBooksDBContext>( options =>
{
    var connectionString = builder.Configuration.GetConnectionString("HHBooksConnection");
    options.UseSqlServer(connectionString);
});
// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddTransient<IBooksServices, BooksServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
