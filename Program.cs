using Microsoft.EntityFrameworkCore;
using MVCTaskProject.Configuration;
using MVCTaskProject.Models;
using MVCTaskProject.Repository;
using MVCTaskProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IMailService, MailService>();


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));


builder.Services.AddSession(options=> options.IdleTimeout = TimeSpan.FromDays(10));

builder.Services.AddTransient<ILoginRepository , LoginRepository>();
builder.Services.AddTransient<IHomeRepository , HomeRepository>();
builder.Services.AddTransient<IProfileRepository , ProfileRepository>();

builder.Services.AddDbContext<ApplicationDbContext>
    (Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MVCTaskProjectDb")));

builder.Services.AddDbContext<ApplicationDbContext>
    (Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MVCTaskDb")));

builder.Services.AddDbContext<ApplicationDbContext>
   (Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("UserProfileDb")));




var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Redirect to an error handling action in production
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
