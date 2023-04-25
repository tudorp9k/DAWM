using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GradeDto = DataLayer.Dtos.GradeDto;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;
        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("/register")]
        public IActionResult Register(UserAuthDto payload)
        {
            var result = userService.RegisterUser(payload);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginDto payload)
        {
            var token = userService.ValidateUser(payload);

            if (token == null)
            {
                return BadRequest();
            }

            return Ok(token);
        }

        [Authorize(Roles = "Student,Teacher")]
        [HttpGet("/grades")]
        public IActionResult GetGrades()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var userRole = User.FindFirst("UserRole").Value;

            if (userRole == "Student")
            {
                var grades = userService.GetGradesForStudent(userId);
                return Ok(grades);
            }
            else if (userRole == "Teacher")
            {
                var grades = userService.GetGradesForAllStudents();
                return Ok(grades);
            }

            return BadRequest();
        }
    }
}
