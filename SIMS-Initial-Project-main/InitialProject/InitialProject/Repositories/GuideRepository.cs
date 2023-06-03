using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    internal class GuideRepository
    {
        private readonly GradeGuideRepository gradeGuideRepository;
        public GuideRepository() {

         gradeGuideRepository = new GradeGuideRepository();
        }

        public int LanguageGrades()
        {
            List<GradeGuide> grades = new List<GradeGuide>();
            grades = gradeGuideRepository.GetAll();
            List<int> languageGrades = new List<int>();
            for(int i = 0; i < grades.Count; i++)
            {
                languageGrades.Add(grades[i].GuideLanguage);
            }

            return languageGrades.Sum()/languageGrades.Count();

        }

        public int CountLanguageGrades()
        {
            int numberOfGrades = 0;
            List<GradeGuide> grades = gradeGuideRepository.GetAll();
            
            foreach(GradeGuide grade in grades)
            {
                numberOfGrades++;
            }

            return numberOfGrades;
        }

        public string SuperGuide()
        {
            if(CountLanguageGrades() > 19)
            {
                if (LanguageGrades() > 4)
                {
                    return "SUPER GUIDE";
                }
                else
                    return "Guide";

            }
            return "Guide";
        }

       

    }
}
