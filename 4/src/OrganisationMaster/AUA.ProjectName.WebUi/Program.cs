using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.WebUi.AppConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configuration();

builder.Services.AddControllersWithViews();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(AppConsts.ExceptionHandler);

    app.UseHsts();
}

app.Configuration();

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();
//app.MapDefaultControllerRoute();
//app.MapControllerRoute(
//    //name: "default",
//    pattern: "{controller=Home}/{action=Index}/{slug?}");

app.Run();