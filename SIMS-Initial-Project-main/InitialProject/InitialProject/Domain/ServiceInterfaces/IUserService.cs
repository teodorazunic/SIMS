using InitialProject.Domain.Models;

namespace InitialProject.Domain.ServiceInterfaces
{
    public interface IUserService
    {
        public User GetByUsername(string username);
    }
}
