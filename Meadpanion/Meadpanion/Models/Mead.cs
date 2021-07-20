﻿using System;
using System.Collections.Generic;
using System.Text;
using Meadpanion.SQL;

namespace Meadpanion.Models
{
    public class Mead
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RecipeID { get; set; }
        public bool Active { get; set; }
        public float Amount { get; set; }
        public List<MeadEvents> Events { get; set; }
        public List<Readings> Readigns { get; set; }
        public string Note { get; set; }
    }
}
