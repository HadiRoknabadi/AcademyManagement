using Microsoft.EntityFrameworkCore;
using AcademyManagement.Persistence.Contexts;
using AcademyManagement.Infrastructure.IdentityConfigs;
using GoogleReCaptcha.V3.Interface;
using GoogleReCaptcha.V3;
using AcademyManagement.Infrastructure.Senders;
using FluentValidation;
using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.DTOs.Common;
using AcademyManagement.Infrastructure.MappingProfile;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Services.Implementations;
using FluentValidation.AspNetCore;
using AcademyManagement.Application.Services.Interfaces.Contexts;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

#region Config Database

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

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

#region Config Services
builder.Services.AddScoped<IDatabaseContext,DataBaseContext>();
builder.Services.AddScoped<IPreRegisterationService, PreRegisterationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISenderService, SenderService>();
builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

#endregion

#region Fluent Validation

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddTransient<IValidator<PreRegisterationDTO>, PreRegisterationDTOValidator>();
builder.Services.AddTransient<IValidator<LoginDTO>, LoginDTOValidator>();
builder.Services.AddTransient<IValidator<CaptchaDTO>, CaptchaDTOValidator>();


#endregion

#region Auto Mapper

builder.Services.AddAutoMapper(typeof(PreRegisterationMappingProfile));

#endregion

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
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.Run();
