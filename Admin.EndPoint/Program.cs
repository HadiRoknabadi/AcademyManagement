using AcademyManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

#region Add MVC And Desabling EndpointRouting
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
#endregion

#region Config Database

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});


#endregion

#region Html Encoder

builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));

#endregion




var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    app.UseMvcWithDefaultRoute();

    endpoints.MapControllerRoute(
name: "default",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

});


app.Run();
