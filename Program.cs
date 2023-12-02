using sales_up.Data;
using Microsoft.EntityFrameworkCore;
using sales_up.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TasksDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ITaskService,  TaskService>();


builder.WebHost.UseUrls("http://*:80"); 

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
        var context = serviceScope.ServiceProvider.GetService<TasksDbContext>();
        context.Database.Migrate();
        bool dbReady = false;
        while (!dbReady)
        {
            try
            {
                context.Database.Migrate();
                dbReady = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Waiting for database: {ex.Message}");
                System.Threading.Thread.Sleep(5000);
            }
        }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDo}/{action=Index}/{id?}");

app.Run();
