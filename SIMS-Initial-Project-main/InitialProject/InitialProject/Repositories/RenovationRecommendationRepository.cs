using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repositories
{
    public class RenovationRecommendationRepository : IRenovationRecommendationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovationrecommendation.csv";

        private readonly Serializer<RenovationRecommendation> _serializer;

        private List<RenovationRecommendation> _renovationRecommendations;

        public RenovationRecommendationRepository()
        {
            _serializer = new Serializer<RenovationRecommendation>();
            _renovationRecommendations = _serializer.FromCSV(FilePath);
        }

        private int GetLastId()
        {
            if (_renovationRecommendations != null && _renovationRecommendations.Count > 0)
            {
                return _renovationRecommendations.Max(review => review.Id);
            }
            return 0;
        }

        public void Save(RenovationRecommendation item)
        {
            int itemId = GetLastId() + 1;
            item.Id = itemId;
            _renovationRecommendations.Add(item);
            _serializer.ToCSV(FilePath, _renovationRecommendations);
        }
    }
}
