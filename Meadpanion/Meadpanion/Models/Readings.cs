using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Meadpanion.Models
{
    public class Readings
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public float GravityReading { get; set; }
    }
}
