using AcademyManagement.Application.Services.Implementations;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Services.Interfaces.Contexts;
using AcademyManagement.Infrastructure.IdentityConfigs;
using AcademyManagement.Infrastructure.MappingProfile;
using AcademyManagement.Persistence.Contexts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

#region  Add Identity

builder.Services.AddIdentityService(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

#endregion

#region Html Encoder

builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));

#endregion

#region Fluent Validation

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddTransient<IValidator<AddUserDTO>, AddUserDTOValidator>();



#endregion

#region Config Services

builder.Services.AddScoped<IDatabaseContext, DataBaseContext>();
builder.Services.AddScoped<IUserService, UserService>();


#endregion

#region Auto Mapper

builder.Services.AddAutoMapper(typeof(PreRegisterationMappingProfile));

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
