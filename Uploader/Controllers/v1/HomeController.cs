using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploaderService.Client;
using UploaderService.Helpers;

namespace Uploader.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IUploadHandler _uploadHandler;

        public HomeController(IUploadHandler uploadHandler)
        {
            _uploadHandler = uploadHandler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            var result = await _uploadHandler.UploadFile(file, option =>
            {
                option.DirectoryMaker = DirectoryFactory.MakeDirectory("Document", "Anees", "CV");
                option.IsChangingNameAllowed = true;
                option.MaxAllowedSizeInKb = 100;
            });


            return Ok(result);
        }
    }
}
