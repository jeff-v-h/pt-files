using PTFiles.Application.Features.Patients.GetPatient;
using PTFiles.Application.Features.Patients.GetPatients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PTFiles.Application.Features.Patients.CreatePatient;
using PTFiles.Application.Features.Patients.UpdatePatient;
using PTFiles.Application.Features.Patients.DeletePatient;

namespace PTFiles.Web.Controllers
{
    [ApiController]
    public class PatientsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetPatientsVm>> Get()
        {
            return await Mediator.Send(new GetPatientsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPatientVm>> Get(int id)
        {
            return await Mediator.Send(new GetPatientQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreatePatientCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePatientCommand command)
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
            await Mediator.Send(new DeletePatientCommand { Id = id });

            return NoContent();
        }
    }
}
