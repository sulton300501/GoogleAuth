using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

}).AddCookie()
  .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
  {
      options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
      options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
  })
  .AddFacebook(options =>
  {
      options.AppId = builder.Configuration.GetSection("Facebook:AppId").Value;
      options.AppSecret = builder.Configuration.GetSection("Facebook:AppSecret").Value;

      options.CallbackPath = "/signin-facebook";
  });








// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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


