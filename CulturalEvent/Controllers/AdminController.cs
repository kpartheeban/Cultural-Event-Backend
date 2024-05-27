using CulturalEvent.Data;
using CulturalEvent.DTO;
using CulturalEvent.Model;
using CulturalEvent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulturalEvent.Controllers
{
    // it defines route for controller in this case route will be (api/Admin)
    [Route("api/[controller]")] 
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly CulturalsDbContext _dbContext;

        public AdminController(IAdminRepository adminRepository, CulturalsDbContext dbContext)
        {
            _adminRepository = adminRepository;
            _dbContext = dbContext;
        }

        //Login
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] AdminLoginDto adminLoginDto)
        {
            try
            {
                var result = _adminRepository.Login(adminLoginDto);
                if (result == "Incorrect Credentails for admin")
                    return Ok("Incorrect Credentials for admin");
                else
                    return Ok(result);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while logging in the admin.");
            }
        }

        // add events
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddEvents([FromBody] Event _event)
        {
            try
            {
                _adminRepository.InsertAsync(_event);
                _dbContext.SaveChanges();
                return Ok("Event Added Successfully");
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while adding the event.");
            }
        }

        //Update the Event table
        [HttpPut("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateEvents([FromBody] Event _event)
        {
            try
            {
                _adminRepository.UpdateAsync(_event);
                return Ok(_event);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while updating the event.");
            }
        }

        // GET: all fields
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisteredDetails()
        {
            try
            {
                var res = await _adminRepository.GetAll();
                return Ok(res);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while retrieving the registered details.");
            }
        }

        // GET: all fields
        [HttpGet("[action]")]
        public async Task<IActionResult> Events()
        {
            try
            {
                var res = await _adminRepository.GetEvents();
                return Ok(res);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while retrieving the events.");
            }
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult StudentId(string StudentId)
        {
            try
            {
                if (StudentId == null)
                    return NotFound("Invalid Student Id");

                var res = _adminRepository.GetByStudentId(StudentId);
                return Ok("Retrieved Successfully");
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while retrieving the student details.");
            }
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                if (id != 0)
                {
                    _adminRepository.DeleteAsync(id);
                    _dbContext.SaveChanges();
                    return Ok("Successfully Deleted");
                }
                return Ok("Enter the valid Event ID");
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while deleting the event.");

            }
        }
    }
}