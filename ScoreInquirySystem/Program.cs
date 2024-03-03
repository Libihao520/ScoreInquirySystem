using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ScoreInquirySystem.Config;
using ScoreInquirySystem.Data;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //jwt鉴权在swagger中的应用
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"}
            },new string[] { }
        }
    });
});


builder.Register();

builder.Services.AddDbContext<TestContext>(p =>
{
    p.UseMySql(builder.Configuration.GetConnectionString("Mysql"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
});
builder.Services.AddSingleton<DapperContext>(sp => new DapperContext(builder.Configuration.GetConnectionString("Mysql")));  
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//通过 ASP.NET Core 中配置的授权认证，读取客户端中的身份标识(Cookie,Token等)并解析出来，存储到 context.User 中
app.UseAuthentication();
//判断当前访问 Endpoint (Controller或Action)是否使用了 [Authorize]以及配置角色或策略，然后校验 Cookie 或 Token 是否有效
app.UseAuthorization();

app.MapControllers();
app.Run();




