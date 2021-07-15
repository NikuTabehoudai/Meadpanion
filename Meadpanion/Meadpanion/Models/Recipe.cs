using System;
using System.Collections.Generic;
using System.Text;

namespace Meadpanion.Models
{
    class Recipe
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
    }   
}
