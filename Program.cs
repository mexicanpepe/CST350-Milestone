using CST350_Minesweeper.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<SecurityDAO>();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(); // Add this line for authorization services

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization(); // Ensure this is present

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

