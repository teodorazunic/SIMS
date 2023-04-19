using InitialProject.Domain.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    internal class GradeGuide : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }

        public int GuideKnowledge { get; set; }

        public int GuideLanguage { get; set; }

        public int Overall { get; set; }

        public string Comment { get; set; }

        public string Validation { get; set; }

        public string PictureUrl { get; set; }

        public GradeGuide(int id,int guestId, int guideKnowledge, int guideLanguage, int overall, string comment, string validation, string pictureUrl)
        {
            Id = id;
            GuestId = guestId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            Overall = overall;
            Comment = comment;
            Validation = "Valid";
            PictureUrl = pictureUrl;

        }

        public GradeGuide()
        {
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),GuestId.ToString(), GuideKnowledge.ToString(), GuideLanguage.ToString(), Overall.ToString(), Comment, Validation, PictureUrl };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            GuideKnowledge = Convert.ToInt32(values[2]);
            GuideLanguage = Convert.ToInt32(values[3]);
            Overall = Convert.ToInt32(values[4]);
            Comment = values[5];
            Validation = values[6];
            PictureUrl = values[7];
        }
    }
}
