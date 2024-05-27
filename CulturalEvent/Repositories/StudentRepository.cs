using CulturalEvent.Data;
using CulturalEvent.DTO;
using CulturalEvent.Model;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CulturalEvent.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CulturalsDbContext _context;
        private readonly IConfiguration configuration;
        public StudentRepository(CulturalsDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        //Register
        public string RegisterAsync(Student studentRegDto)
        {
            var studentExist = _context.Students.FirstOrDefault(x=>x.StudentEmail == studentRegDto.StudentEmail && x.Password == studentRegDto.Password);
            if (studentExist == null) 
            {
                
                _context.Students.Add(studentRegDto);
                _context.SaveChanges();
                return "Student Registration Successfull!!!!";
            }
            else 
            {
                return "Student Details already exists ";
            }
        }
        //Login
        public string Login(StudentLoginDto studentLoginDto)
        {
            var studentExist = _context.Students.FirstOrDefault(t => t.StudentEmail == studentLoginDto.StudentEmail && t.Password == studentLoginDto.Password);

            if (studentExist != null)
            {
                if(studentExist.Password == studentLoginDto.Password && studentExist.StudentEmail==studentLoginDto.StudentEmail) 
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email,studentExist.StudentEmail),
                        
                        new Claim(ClaimTypes.Role,"Student")
                    };

                    var token = new JwtSecurityToken
                        (
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(2),
                        signingCredentials: credentials
                        );
                    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwt;
                }
                else 
                    return "Incorrect Password";
                
            }
            else
            {
                return "Account doesn't exists. Create One";
            }

        }
        public string RegisterEvent(Registration registration)
        {
            var userExist = _context.Registrations.FirstOrDefault(a => a.StudentId == registration.StudentId && a.Name == registration.Name);
            if (userExist != null) {
                return "StudentId and EventName already Exist";
            }
            else {
                var eventExist = _context.Events.FirstOrDefault(x => x.EventId == registration.EventId && x.EventName == registration.Name);
                if (eventExist != null)
                {
                    _context.Registrations.Add(registration);
                    _context.SaveChanges();
                    return "Event Registration Successfully Completed";
                }
                else
                {
                    return "Invalid Event Id";
                }
            }
            

        }
        public IEnumerable<Event> GetEvents() 
        {
            return _context.Events.ToList();
        }

        public IEnumerable<Registration> GetByStudentId(string StudentId)
        {
            var res = _context.Registrations.Where(x => x.StudentId == StudentId).ToList();
            return res;
        }
    }
}
