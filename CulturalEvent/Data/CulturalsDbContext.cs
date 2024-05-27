using CulturalEvent.Model;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvent.Data
{
    public class CulturalsDbContext : DbContext
    {
        public CulturalsDbContext(DbContextOptions<CulturalsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Student> Students { get; set; }

       
    }

}
