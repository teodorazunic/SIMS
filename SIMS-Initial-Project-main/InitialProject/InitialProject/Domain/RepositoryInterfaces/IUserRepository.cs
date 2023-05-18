using InitialProject.Domain.Model;
using InitialProject.Domain.Models;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public User GetByUsername(string username);

        public User CheckIfSuperGuest(User user);
    }
}
