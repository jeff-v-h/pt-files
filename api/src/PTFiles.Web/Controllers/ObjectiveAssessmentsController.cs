using PTFiles.Application.Features.ObjectiveAx.GetObjectiveAssessment;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PTFiles.Web.Controllers
{
    [ApiController]
    [Route("")]
    public class ObjectiveAssessmentsController : ApiControllerBase
    {
        [HttpGet("consultations/{consultationId}/objective")]
        public async Task<ActionResult<GetObjectiveAssessmentVm>> Get(int consultationId)
        {
            return await Mediator.Send(new GetObjectiveAssessmentQuery { ConsultationId = consultationId });
        }
    }
}

