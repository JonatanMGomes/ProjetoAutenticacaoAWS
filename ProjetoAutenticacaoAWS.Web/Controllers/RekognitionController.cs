using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAutenticacaoAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RekognitionController : ControllerBase
    {
        private readonly AmazonRekognitionClient _rekognitionClient;
        public RekognitionController(AmazonRekognitionClient rekognitionClient)
        {
            _rekognitionClient = rekognitionClient;
        }
        [HttpPost("comparar")] 
        public async Task<IActionResult> CompararRosto(string nomeArquivoS3, IFormFile fotoLogin)
        {
            using (var memStream = new MemoryStream())
            {
                var request = new CompareFacesRequest();

                var requestSource = new Image()
                {
                    S3Object = new S3Object()
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
                return Ok(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AnalisarRosto(string nomeArquivo)
        {
            var request = new DetectFacesRequest();
            var imagem = new Image();

            var s3Object = new S3Object()
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
                return Ok(response);
            }
            return BadRequest();
        }
    }
}