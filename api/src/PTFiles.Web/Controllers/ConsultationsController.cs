using PTFiles.Application.Features.Consultations.GetConsultation;
using PTFiles.Application.Features.Consultations.UpdateConsultation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using PTFiles.Application.Features.Consultations.GetConsultations;
using PTFiles.Application.Features.Consultations.CreateConsultation;
using PTFiles.Application.Features.Consultations.DeleteConsultation;

namespace PTFiles.Web.Controllers
{
    [ApiController]
    public class ConsultationsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GetConsultationBaseVm>>> GetList([FromQuery] int casefileId)
        {
            return await Mediator.Send(new GetConsultationsQuery { CasefileId = casefileId });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetConsultationVm>> Get(int id)
        {
            return await Mediator.Send(new GetConsultationQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateConsultationCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateConsultationCommand command)
        {
            var newId = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = newId }, newId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteConsultationCommand { Id = id });

            return NoContent();
        }
    }
}
