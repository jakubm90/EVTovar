using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EVTovar.Models
{
    public class Item : BaseItem
    {
        public DateTime Modified { get; set; }
        public int Weight { get; set; }
        public decimal Price { get; set; }

    }
}
