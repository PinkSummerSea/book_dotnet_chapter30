var builder = WebApplication.CreateBuilder();

// add services to the builder
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AutoLot");
builder.Services.AddDbContextPool<ApplicationDbContext>(
    options=>options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60))
);
builder.Services.AddRepositories();

var app = builder.Build();

// configure http request pipeline
// if (app.Environment.IsDevelopment())
// {
    
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();