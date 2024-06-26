using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Questioning.Business;
using Questioning.Data;
using Questioning.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();
;

//Bad practise
builder.Services
    .AddDbContext<ExamDbContext>(o =>
        o.UseSqlite(@"Data Source=Encrypted.db;Password=MyEncryptionKey")
    );
    

builder.Services.AddValidatorsFromAssemblyContaining<ExamManager>();

builder.Services.AddScoped<IExamDbContext, ExamDbContext>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<ExamManager>();
builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<ExamDbContext>())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    context.Database.Migrate();
    logger.LogInformation("Database migrated!");
    scope.ServiceProvider.GetRequiredService<DataSeeder>().Seed();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program{}