using CulturalEvent.Data;
using CulturalEvent.DTO;
using CulturalEvent.Model;
using CulturalEvent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulturalEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly CulturalsDbContext _context;

        public StudentController(CulturalsDbContext context, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _context = context;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] Student studentRegDto)
        {
            try
            {
                var res = _studentRepository.RegisterAsync(studentRegDto);
                if (res == "Student Registration Successfull!!!!")
                    return Ok("User Registration Successfull!!!");
                else
                    return BadRequest("User Already Exist!");
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while registering the student.");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] StudentLoginDto studentLoginDto)
        {
            try
            {
                var res1 = _studentRepository.Login(studentLoginDto);
                if (res1 == "Account doesn't exists. Create One")
                    return Ok("Account Doesn't Exist");
                else if (res1 == "Incorrect Password")
                    return Ok("Incorrect Password");
                else
                    return Ok(res1);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while logging in the student.");
            }
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Student")]
        public IActionResult EventRegistration([FromBody] Registration registration)
        {
            try
            {
                var res2 = _studentRepository.RegisterEvent(registration);
                if (res2 == "Invalid Event Id")
                    return Ok("Invalid Event Id");
                else if (res2 == "StudentId and EventName already Exist")
                    return Ok("StudentId and EventName already Exist");
                else
                    return Ok(res2);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while registering for the event.");
            }
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Student")]
        public IActionResult GetAllEvents()
        {
            try
            {
                var res = _studentRepository.GetEvents().ToList();
                return Ok(res);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while retrieving the events.");
            }
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Student")]
        public IActionResult StudentId(string StudentId)
        {
            try
            {
                if (StudentId == null)
                    return NotFound("Invalid Student Id");

                var res = _studentRepository.GetByStudentId(StudentId);
                return Ok(res);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while retrieving the student details.");
            }
        }
    }
}