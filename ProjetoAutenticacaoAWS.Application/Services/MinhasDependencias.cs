using Amazon.Rekognition;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoAutenticacaoAWS.Lib.Data;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Services;

namespace ProjetoAutenticacaoAWS.Application.Services
{
    public static class MinhasDependencias
    {
        public static void InjetarDependencias(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddDbContext<AutenticacaoContext>(
                    conn => conn.UseNpgsql(configuration.GetConnectionString("Autenticacao_DB"))
                    .UseSnakeCaseNamingConvention()
                    );

            collection.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            collection.AddScoped<IUsuarioApplication, UsuarioApplication>();
            collection.AddScoped<ServicesAWS>();

            var awsOptions = configuration.GetAWSOptions();
            awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
            collection.AddDefaultAWSOptions(awsOptions);

            collection.AddAWSService<IAmazonS3>();
            collection.AddScoped<AmazonRekognitionClient>();
        }
    }
}