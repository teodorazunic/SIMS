using InitialProject.Domain;
using InitialProject.Domain.Models;
using InitialProject.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace InitialProject.WPF.ViewModels.Guest1
{
    public class AllForumsViewModel
    {
        public User User { get; set; }

        public Forum Selected { get; set; }


        private IForumService ForumService;

        public AllForumsViewModel(User user)
        {
            User = user;
            ForumService = Injector.CreateInstance<IForumService>();
        }

        public List<Forum> ShowAllForums()
        {
            return ForumService.GetAll();
        }

        public List<Forum> ShowMyForums()
        {
            return ForumService.GetAllForumsByCreatorId(User.Id);
        }

        public List<Forum> OnLoad()
        {
            return this.ShowAllForums();
        }
    }
}
