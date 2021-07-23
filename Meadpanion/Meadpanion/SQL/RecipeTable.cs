using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Meadpanion.SQL
{
    public class RecipeTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Note { get; set; }
    }
}
