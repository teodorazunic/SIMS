using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;

namespace InitialProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }


        public User GetByUsername(string username)
        {
            User user =  _userRepository.GetByUsername(username);

            if(user != null && user.Role == UserRole.guest1)
            {
                user = _userRepository.CheckIfSuperGuest(user);
            }

            return user;
        }
    }
}
