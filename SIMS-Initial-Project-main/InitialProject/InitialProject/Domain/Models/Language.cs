using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class Language
    {
        public string Name { get; set; }


        public Language() { }

        public Language(string name)
        {
            Name = name;
            
        }

    }
}
