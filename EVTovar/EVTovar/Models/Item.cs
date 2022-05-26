using System;
using System.Collections.Generic;
using System.Text;

namespace EVTovar.Models
{
    public class Item : BaseModel
    {
        public int ManufacturerID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string Note { get; set; }

    }
}
