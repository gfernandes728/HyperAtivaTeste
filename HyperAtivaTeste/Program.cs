using HyperAtivaTeste.API.Services.Interfaces;
using HyperAtivaTeste.API.Services;
using HyperAtivaTeste.Domains.Interfaces.Repository;
using HyperAtivaTeste.Domains.Interfaces.Services;
using HyperAtivaTeste.Infra.Repository;
using HyperAtivaTeste.Infra.Services;
using HyperAtivaTeste.Infra;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMvc(options => { options.EnableEndpointRouting = false; }).AddJsonOptions(options => { });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "HyperAtivaTeste API", Version = "v1" });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b5ae3319cb2566e7938c4a459d351b75")),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

var healthCheck = builder.Services.AddHealthChecksUI(setupSettings: x =>
{
    x.DisableDatabaseMigrations();
    x.MaximumHistoryEntriesPerEndpoint(6);
}).AddInMemoryStorage();

var builderHealthCheck = healthCheck.Services.AddHealthChecks();

builderHealthCheck.AddProcessAllocatedMemoryHealthCheck(500 * 1024 * 1024, "Process Memory", tags: new[] { "self" });
builderHealthCheck.AddPrivateMemoryHealthCheck(1500 * 1024 * 1024, "Private memory", tags: new[] { "self" });

builderHealthCheck.AddSqlServer(
    connectionString: "",
    name: "SQL Server",
    healthQuery: "Select 1",
    tags: new[] { "services" }
);


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<DbDapperContext>();

builder.Services.AddScoped<IUserApiService, UserApiService>();
builder.Services.AddScoped<ICreditCardApiService, CreditCardApiService>();
builder.Services.AddScoped<IAuthorizationApiService, AuthorizationApiService>();
builder.Services.AddScoped<ILogApiService, LogApiService>();

builder.Services.AddScoped<ICreditCardService, CreditCardService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

