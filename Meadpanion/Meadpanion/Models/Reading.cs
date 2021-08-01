using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Meadpanion.Models
{
    public class Reading
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int ID { get; set; }
        [Indexed]
        public int MeadId { get; set; }
        public DateTime Date { get; set; }
        public float GravityReading { get; set; }
        public string Note { get; set; }
        public string ABV { get; set; }
        public bool OriginalGravity { get; set; }
    }
}
