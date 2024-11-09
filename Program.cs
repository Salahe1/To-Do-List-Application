using ToDoList.Filters;
using ToDoList.Services;

var builder = WebApplication.CreateBuilder(args);


//// Read configuration from appsettings.json
//builder.Configuration.ReadFrom.Configuration(builder.Configuration); // Corrected line

//// Configure Serilog logging
//builder.Host.UseSerilog((context, services, configuration) => configuration
//    .ReadFrom.Configuration(builder.Configuration)); // Corrected line


// Add services to the container.
builder.Services.AddControllersWithViews(opt => {
    opt.Filters.Add<LogFilter>();
    opt.Filters.Add<ThemeFilter>();   
 });

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(
    Opt => {
        Opt.IdleTimeout = TimeSpan.FromHours(5);
        Opt.Cookie.HttpOnly = true;
        Opt.Cookie.Name = "SessionId";
    });

builder.Services.AddScoped<ITodoService, TodoService>(); // Add these lines
builder.Services.AddScoped<SessionTodoStorage>();

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
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.Run();
