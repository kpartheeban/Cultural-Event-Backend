using CulturalEvent.Model;

namespace CulturalEvent.Repositories
{
    public interface IRegistrationRepository
    {
        IEnumerable<Registration> GetByStudentId(string studenId);
        Task InsertAsync(Registration registration);
        Task<IEnumerable<Event>> GetEvent();
        Task SaveAsync();
    }
}
