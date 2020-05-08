using PTFiles.Application.Features.CaseFiles.GetCaseFile;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PTFiles.Web.Controllers
{
    [ApiController]
    public class CaseFilesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCaseFileVm>> Get(int id)
        {
            return await Mediator.Send(new GetCaseFileQuery { Id = id });
        }
    }
}
