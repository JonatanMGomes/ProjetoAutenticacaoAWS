using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using ProjetoAutenticacaoAWS.Lib.MyExceptions;

namespace ProjetoAutenticacaoAWS.Services
{
    public class ServicesAWS
    {
        private readonly IAmazonS3 _amazonS3;
        private static readonly List<string> _extensoesImagem =
        new List<string>() { "image/jpeg", "image/png" };
        private readonly AmazonRekognitionClient _rekognitionClient;
        public ServicesAWS(IAmazonS3 amazonS3, AmazonRekognitionClient rekognitionClient)
        {
            _amazonS3 = amazonS3;
            _rekognitionClient = rekognitionClient;
        }
        public async Task<string> SalvarNoS3(IFormFile imagem)
        {
            if (!_extensoesImagem.Contains(imagem.ContentType))
            {
                throw new TipoArquivoInvalidoException("Tipo de arquivo inválido!");
            }
            using (var streamDaImagem = new MemoryStream())
            {
                await imagem.CopyToAsync(streamDaImagem);
                var request = new PutObjectRequest();
                request.Key = "recFacial " + imagem.FileName;
                request.BucketName = "imagens-aulas";
                request.InputStream = streamDaImagem;

                var resposta = await _amazonS3.PutObjectAsync(request);
                return request.Key;
            }
        }
        public async Task DeletarNoS3(string nomeBucket, string nomeArquivo)
        {
            var response = await _amazonS3.DeleteObjectAsync("imagens-aulas", nomeArquivo);
        }
        public async Task<bool> ValidarImagem(string nomeArquivo)
        {
            var request = new DetectFacesRequest();
            var imagem = new Image();

            var s3Object = new Amazon.Rekognition.Model.S3Object()
            {
                Bucket = "imagens-aulas",
                Name = nomeArquivo

            };

            imagem.S3Object = s3Object;
            request.Image = imagem;
            request.Attributes = new List<string>() { "ALL" };

            var response = await _rekognitionClient.DetectFacesAsync(request);

            if (response.FaceDetails.Count == 1 && response.FaceDetails.First().Eyeglasses.Value == false)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> VerificarImagem(string nomeArquivoS3, IFormFile fotoLogin)
        {
            using (var memStream = new MemoryStream())
            {
                var request = new CompareFacesRequest();

                var requestSource = new Image()
                {
                    S3Object = new Amazon.Rekognition.Model.S3Object()
                    {
                        Bucket = "imagens-aulas",
                        Name = nomeArquivoS3
                    }
                };

                await fotoLogin.CopyToAsync(memStream);
                var requestTarget = new Image()
                {
                    Bytes = memStream
                };

                request.SourceImage = requestSource;
                request.TargetImage = requestTarget;

                var response = await _rekognitionClient.CompareFacesAsync(request);
                if (response.FaceMatches.Count == 1 && response.FaceMatches.First().Similarity >= 90)
                {
                    return true;
                }
                return false;
            }
        }
    }
}