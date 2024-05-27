using CulturalEvent.DTO;
using CulturalEvent.Model;

namespace CulturalEvent.Repositories
{
    public interface IAdminRepository
    {
        string Login(AdminLoginDto adminLoginDto);
        Task<IEnumerable<Registration>> GetAll();
        Task<IEnumerable<Event>> GetEvents();   
        IEnumerable<Registration> GetByStudentId(string StudentId);
        void InsertAsync(Event events);
        void UpdateAsync(Event events);
        void DeleteAsync(int id);


        //Task<Admin> GetAdminByEmailAsync(string email);
        //Task<Admin> RegisterAdminAsync(AdminDto adminDto);
        ////Task<IEnumerable<Event>> GetEvents();
    }
}
