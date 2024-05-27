using CulturalEvent.Data;
using CulturalEvent.Model;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvent.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly CulturalsDbContext _context;
        private readonly IConfiguration _configuration;
        public RegistrationRepository(IConfiguration configuration, CulturalsDbContext _context)
        {
            this._context = _context;
            _configuration = configuration;
        }

        public IEnumerable<Registration> GetByStudentId(string studentId)
        {
            var res = _context.Registrations.Where(x => x.StudentId == studentId).ToList();
            return res;
        }

        public async Task InsertAsync(Registration registration)
        {
            var result = _context.Events.FirstOrDefault(x => x.EventId == registration.EventId);// && x.ToStation == bookmaster.ToStation);
            if (result != null)
            {

                await _context.Registrations.AddAsync(registration);
                await _context.SaveChangesAsync();

            }
        }

        // Save 
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        //display the events


        public async Task<IEnumerable<Event>> GetEvent()
        {
            return await _context.Events.ToListAsync();
        }

        
    }
}
