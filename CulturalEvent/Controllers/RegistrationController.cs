
using CulturalEvent.Data;
using CulturalEvent.Model;
using CulturalEvent.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CulturalEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly CulturalsDbContext _context;
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationController(CulturalsDbContext context, IRegistrationRepository registrationRepository)
        {
            _context = context;
            _registrationRepository = registrationRepository;
        }

        // Get the record by StudentId
        [HttpGet("[action]")]
        //[Authorize(Roles = "User")]
        public IActionResult StudentId(string StudentId)
        {
            if (StudentId == null)
            {
                return NotFound();
            }
            var res = _registrationRepository.GetByStudentId(StudentId);

            return Ok(res);
        }


        [HttpGet("[action]")]
        // [Authorize(Roles = "User")]
        public async Task<IActionResult> Events()
        {
            var res = await _registrationRepository.GetEvent();
            return Ok(res);
        }


    }
}
