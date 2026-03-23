var builder = WebApplication.CreateBuilder();

// add services to the container

builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("AutoLot");
builder.Services.AddDbContextPool<ApplicationDbContext>(
    options=>options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60))
);
builder.Services.AddRepositories();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.Configure<DealerInfo>(builder.Configuration.GetSection(nameof(DealerInfo)));
builder.Services.ConfigureApiServiceWrapper(builder.Configuration);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddWebOptimizer(false,false);
    // builder.Services.AddWebOptimizer(options =>
    // {
    //     options.MinifyCssFiles("AutoLot.Mvc.styles.css");
    //     options.MinifyCssFiles("css/site.css");
    //     options.MinifyJsFiles("js/site.js");
    // });
}
else
{
    builder.Services.AddWebOptimizer(options =>
    {
        options.MinifyCssFiles("AutoLot.Mvc.styles.css");
        options.MinifyCssFiles("css/site.css");
        options.MinifyJsFiles("js/site.js");
    });
}
var app = builder.Build();

//configure http request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    if (app.Configuration.GetValue<bool>("RebuildDataBase"))
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        SampleDataInitializer.InitializeData(dbContext);
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseWebOptimizer();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
// app.MapControllerRoute(
//     name:"default",
//     pattern: "{controller=Home}/{action=Index}/{id?}"
// );
app.Run();