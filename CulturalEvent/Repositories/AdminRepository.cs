using CulturalEvent.Data;
using CulturalEvent.DTO;
using CulturalEvent.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CulturalEvent.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CulturalsDbContext _context;
        private readonly IConfiguration _configuration;
        public AdminRepository(CulturalsDbContext context,IConfiguration configurtion)
        {
            _context = context;
            _configuration = configurtion;
        }
        //    public async Task<Admin> GetAdminByEmailAsync(string email)
        //    {
        //        return await _context.Admins.FirstOrDefaultAsync(a => a.EmailId == email);
        //    }

        //    public async Task<Admin> RegisterAdminAsync(AdminDto adminDto)
        //    {
        //        var admin = new Admin
        //        {
        //            AdminName = adminDto.AdminName,
        //            EmailId = adminDto.EmailId,
        //            Password = adminDto.Password
        //        };
        //        _context.Admins.Add(admin);
        //        await _context.SaveChangesAsync();

        //        return admin;


        //    }
        //}

        //Login
        public string Login(AdminLoginDto adminLoginDto)
        {
            string admin_email = "admin@gmail.com";
            string admin_password = "Admin@123";
            //var adminExist = _context.Admins.FirstOrDefault(t => t.EmailId == admin_email && t.Password == admin_password);

            if (adminLoginDto.EmailId == admin_email && adminLoginDto.Password == admin_password)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                //For payload for JWT
                var claims = new[]
                {
                        new Claim(ClaimTypes.Email,adminLoginDto.EmailId),
                        new Claim(ClaimTypes.Role,"Admin")
                        };

                var token = new JwtSecurityToken
                    (
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials
                    );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                Console.WriteLine("For-Admin    " + jwt);
                return jwt;
            }
            else

            {
                return "Incorrect Credentails for admin";
            }

        }

        // Get all Registered records
        public async Task<IEnumerable<Registration>> GetAll()
        {
            return await _context.Registrations.ToListAsync();
        }

        //display the events
        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }
        public IEnumerable<Registration> GetByStudentId(string StudentId)
        {
            var res = _context.Registrations.Where(x => x.StudentId == StudentId).ToList();
            return res;
        }
        // adding the Event
        public void InsertAsync(Event _event)
        {
            _context.Events.AddAsync(_event);
            _context.SaveChanges();
        }
        //Update the Events
        public void UpdateAsync(Event _event)
        {

            _context.Events.Update(_event);
            _context.SaveChanges();
        }
        public void DeleteAsync(int id)
        {
            var res = _context.Events.Find(id);
            if (res != null)
            {
                _context.Events.Remove(res);
            }
        }
    }
}
