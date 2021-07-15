using System;
using System.Collections.Generic;
using System.Text;

namespace Meadpanion.Models
{
    class Mead
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RecipeID { get; set; }
        public Events Events { get; set; }
        public Readings Readings { get; set; }
        public bool active { get; set; }
        public int Amount { get; set; }
    }
}
