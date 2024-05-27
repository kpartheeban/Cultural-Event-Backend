using CulturalEvent.DTO;
using CulturalEvent.Model;

namespace CulturalEvent.Repositories
{
    public interface IStudentRepository
    {
        string RegisterAsync(Student studentRegDto);
        string Login(StudentLoginDto studentLoginDto);
        string RegisterEvent(Registration registration);
        IEnumerable<Event> GetEvents();
        IEnumerable<Registration> GetByStudentId(string StudentId);
    }
}
