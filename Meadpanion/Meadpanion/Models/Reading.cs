using System;
using System.Collections.Generic;
using System.Text;

namespace Meadpanion.Models
{
    public class Reading
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public float GravityReading { get; set; }
        public string Note { get; set; }
    }
}
