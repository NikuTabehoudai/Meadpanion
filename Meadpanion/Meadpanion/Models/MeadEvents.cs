using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Meadpanion.Models
{
    public class MeadEvents
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int ID { get; set; }
        [Indexed]
        public int MeadId { get; set; }
        public DateTime Date { get; set; }
        public string EventType { get; set; }
        public string Note { get; set; }

        //Step Feeding
        public bool StepFeeding { get; set; }
        public float OldGravity { get; set; }
        public float NewGravity { get; set; }

        //Botteling
        public bool Botteling { get; set; }
        public int Bottles { get; set; }

    }
}
