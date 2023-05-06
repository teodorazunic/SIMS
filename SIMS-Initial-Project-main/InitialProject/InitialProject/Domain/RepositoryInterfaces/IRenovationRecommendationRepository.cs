using InitialProject.Domain.Models;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IRenovationRecommendationRepository
    {
        public void Save(RenovationRecommendation renovationRecommendation);

    }
}
