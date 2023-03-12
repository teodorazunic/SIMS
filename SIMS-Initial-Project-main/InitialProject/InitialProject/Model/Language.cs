using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Language
    {
        public string Name { get; set; }

        public int Id { get; set; }
        
        public Language(){}

        public Language(string name, int id)
        {
            Name = name;
            Id = id;
        }

    }
}
