using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/grades")]
    public class GradesController : ControllerBase
    {
        private readonly GradeService gradeService;

        public GradesController(GradeService gradeService)
        {
            this.gradeService = gradeService ?? throw new ArgumentNullException(nameof(gradeService));
        }

        [HttpPost("add-grades")]
        public ActionResult<GradeAddDto> Add(GradeAddDto grade)
        {
            var result = gradeService.AddGrade(grade);

            if (result == null)
            {
                return BadRequest("Grade cannot be added");
            }

            return Ok(result);
        }
    }
}
