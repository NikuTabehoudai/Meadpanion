using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Meadpanion.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Note { get; set; }
    }   
}
