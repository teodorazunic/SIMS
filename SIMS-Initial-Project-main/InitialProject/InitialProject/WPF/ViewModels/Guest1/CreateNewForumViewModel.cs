using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.ServiceInterfaces;
using System.Collections.Generic;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class CreateNewForumViewModel : MainViewModel
    {
        private IAccommodationRepository _accommodationRepository;
        private IForumService _forumService;

        public List<Location> Locations { get; set; }
        public User User { get; set; }

        private int _locationIndex;
        private string _forumQuestion = "";

        public int LocationIndex
        {
            get => _locationIndex;
            set
            {
                if (value != _locationIndex)
                {
                    _locationIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ForumQuestion
        {
            get => _forumQuestion;
            set
            {
                if (value != _forumQuestion)
                {
                    _forumQuestion = value;
                    OnPropertyChanged();
                }
            }
        }
        public CreateNewForumViewModel(User user)
        {
            User = user;
            _forumService = Injector.CreateInstance<IForumService>();
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            Locations = _accommodationRepository.GetAllLocations();
        }

        public void Save()
        {
            Forum forum = new Forum();
            forum.Question = ForumQuestion;
            forum.CreatorId = User.Id;
            forum.Location = Locations[LocationIndex];
            forum.IsDeleted = false;
            forum.IsVeryUseful = false;

            _forumService.CreateForum(forum);
        }
    }
}
