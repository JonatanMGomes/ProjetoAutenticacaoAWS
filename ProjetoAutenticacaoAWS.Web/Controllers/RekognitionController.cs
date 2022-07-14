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

        [HttpGet]
        public async Task<IActionResult> AnalisarRosto(string nomeArquivo)
        {
            var request = new DetectFacesRequest();
            var imagem = new Image();

            var s3Object = new S3Object(){
                Bucket = "imagens-aulas",
                Name = nomeArquivo
                
            };

            imagem.S3Object =  s3Object;
            request.Image = imagem;        
            request.Attributes = new List<string>(){"ALL"};

            var response = await _rekognitionClient.DetectFacesAsync(request);

            if (response.FaceDetails.Count == 1 && response.FaceDetails.First().Eyeglasses.Value == false)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}