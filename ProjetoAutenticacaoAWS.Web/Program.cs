using Amazon.Rekognition;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacaoAWS.Lib.Data;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AutenticacaoContext>(
        conn => conn.UseNpgsql(builder.Configuration.GetConnectionString("Autenticacao_DB"))
                    .UseSnakeCaseNamingConvention()
    );

builder.Services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
builder.Services.AddDefaultAWSOptions(awsOptions);

builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<AmazonRekognitionClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
