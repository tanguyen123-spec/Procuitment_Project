using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Produ_project.Enitity;
using Produ_project.Helper;
using Produ_project.Repository;
using Produ_project.Secure.Service;
using Produ_project.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//var optionsBuilder = new DbContextOptionsBuilder<Produ_projectContext>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Produ_projectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"));
    //có thể sử dụng câu lệnh dưới đây để notracking toàn cục
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
#region AddSerivce
builder.Services.AddScoped(typeof(IRepository<>), typeof(MyRepository<>));
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IcategoryService, CategoriesService>();
builder.Services.AddScoped<ImainProduct, MainproductService>();
builder.Services.AddScoped<IArtWorkService, ArtWorkService>();
builder.Services.AddScoped<Iquality, QualityService>();
builder.Services.AddScoped<ISuppierInfo, SupplierInfoService>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region Token
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var secretKey = builder.Configuration["AppSettings:SecretKey"];

var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {

            //kiểm tra người cấp token
            ValidateIssuer = false,
            //kiểm tra người nhận token
            ValidateAudience = false,
            //bật chức năng kiểm tra chữ ký (signing key) của issuer
            ValidateIssuerSigningKey = true,
            //chìa khóa bí mật được sử dụng để ký và giải mã token.
            //Cả server và client đều cần chia sẻ chìa khóa này để đảm bảo tính toàn vẹn của token
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            //đặt thời gian hết hạn là zero
            ClockSkew = TimeSpan.Zero,
        };
        //cấu hình nhận token từ header
        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
                Console.WriteLine($"Đã Nhận token");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI BanHang", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
#endregion 

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
