using PTFiles.Application.Features.Casefiles.GetCasefile;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using PTFiles.Application.Features.Casefiles.GetCasefiles;
using PTFiles.Application.Features.Casefiles.CreateCasefile;
using PTFiles.Application.Features.Casefiles.UpdateCasefile;
using PTFiles.Application.Features.Casefiles.DeleteCasefile;

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

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateCasefileCommand command)
        {
            var newId = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = newId }, newId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCasefileCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCasefileCommand { Id = id });

            return NoContent();
        }
    }
}
