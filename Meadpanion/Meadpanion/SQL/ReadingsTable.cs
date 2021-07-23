using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Meadpanion.SQL
{
    public class ReadingsTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Indexed]
        public int MeadID { get; set; }
        public DateTime Date { get; set; }
        public float GravityReading { get; set; }
        public string Note { get; set; }
    }
}
