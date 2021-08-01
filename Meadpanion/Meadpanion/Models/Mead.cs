using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Meadpanion.Models
{
    public class Mead
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int RecipeID { get; set; }
        public bool Active { get; set; }
        public float Amount { get; set; }
        public string Note { get; set; }
    }
}
