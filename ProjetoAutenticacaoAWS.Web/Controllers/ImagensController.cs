using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAutenticacaoAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagensController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        private static readonly List<string> _extensoesImagem =
        new List<string>() { "image/jpeg", "image/png" };
        public ImagensController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        [HttpPost("bucket")]
        public async Task<IActionResult> CriarBucket(string nomeBucket)
        {
            var resposta = await _amazonS3.PutBucketAsync(nomeBucket);
            return Ok(resposta);
        }
        [HttpGet("bucket")]
        public async Task<IActionResult> BuscarBuckets()
        {
            var resposta = await _amazonS3.ListBucketsAsync();
            //return Ok(resposta.Buckets.Select(x => x.BucketName));
            return Ok(resposta);
        }
        [HttpPost()]
        public async Task<IActionResult> CriarImagem(IFormFile imagem)
        {
            if (!_extensoesImagem.Contains(imagem.ContentType))
            {
                return BadRequest("Tipo inválido!");
            }
            using (var streamDaImagem = new MemoryStream())
            {
                await imagem.CopyToAsync(streamDaImagem);
                var request = new PutObjectRequest();
                request.Key = "recFacial "+imagem.FileName;
                request.BucketName = "imagens-aulas";
                request.InputStream = streamDaImagem;

                var resposta = await _amazonS3.PutObjectAsync(request);
                return Ok();
            }
        }
        [HttpDelete()]
        public async Task<IActionResult> DeletarImagem(string nomeDoArquivoNoS3)
        {
            var resposta = await _amazonS3.DeleteObjectAsync("imagens-aulas", nomeDoArquivoNoS3);
            return Ok(resposta);
        }

    }
}