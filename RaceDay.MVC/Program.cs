var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// 1. Enable Session for Login
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 2. Setup HttpClient to talk to the API
builder.Services.AddHttpClient("RaceDayApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5045/"); // MAKE SURE THIS MATCHES YOUR API PORT
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<RaceDay.MVC.Services.ApiService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // Must be before Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();