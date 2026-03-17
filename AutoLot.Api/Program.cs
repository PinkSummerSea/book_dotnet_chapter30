using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder();

// add services to the builder
builder.Services.AddControllers(config => config.Filters.Add(new CustomExceptionFilterAttribute(builder.Environment)))
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy=null;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive=true;
    options.JsonSerializerOptions.WriteIndented=true;
    options.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles;
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter=true;
    //options.SuppressConsumesConstraintForFormFileParameters=true;
    options.SuppressMapClientErrors=false;
    options.ClientErrorMapping[StatusCodes.Status404NotFound].Link="https://httpstatus.com/404";
    options.ClientErrorMapping[StatusCodes.Status404NotFound].Title="Invalid Location";
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Description = "Basic Authentication"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "basic",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] {}
        }
    });
});
//builder.Services.AddAutoLotApiVersionConfiguration(new ApiVersion(1,0));

var connectionString = builder.Configuration.GetConnectionString("AutoLot");
builder.Services.AddDbContextPool<ApplicationDbContext>(
    options=>options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60))
);
builder.Services.AddRepositories();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection(nameof(SecuritySettings)));
builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication",null);
var app = builder.Build();

// configure http request pipeline
// if (app.Environment.IsDevelopment())
// {
    
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
// add cors policy
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();