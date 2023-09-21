using Microsoft.EntityFrameworkCore;
using Task1.DAO;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SampleContext>(options =>
    options.UseInMemoryDatabase("SampleDatabase"));

var application = builder.Build();

if (!application.Environment.IsDevelopment())
{
    application.UseExceptionHandler("/Error");
}
else application.UseDeveloperExceptionPage();

application.UseHsts();
application.UseHttpsRedirection();
application.UseStaticFiles();
application.UseRouting();
application.UseAuthorization();
application.MapDefaultControllerRoute();
application.Services.CreateAsyncScope().ServiceProvider
    .GetService<SampleContext>().InitializeData();

await application.RunAsync();