using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EVTovar.Models
{
    public class Item : BaseItem
    {
        public DateTime Modified { get; set; }
        public float Weight { get; set; }
        public int Price { get; set; }

    }
}
