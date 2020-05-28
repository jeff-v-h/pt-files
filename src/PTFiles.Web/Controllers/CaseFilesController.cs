using PTFiles.Application.Features.Casefiles.GetCasefile;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using PTFiles.Application.Features.Casefiles.GetCasefiles;

namespace PTFiles.Web.Controllers
{
    [ApiController]
    public class CaseFilesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GetCasefileVm>>> GetList([FromQuery] int patientId)
        {
            return await Mediator.Send(new GetCasefilesQuery { patientId = patientId });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCasefileVm>> Get(int id)
        {
            return await Mediator.Send(new GetCasefileQuery { Id = id });
        }
    }
}
