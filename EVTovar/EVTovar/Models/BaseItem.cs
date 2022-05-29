using System;
using System.Collections.Generic;
using System.Text;

namespace EVTovar.Models
{
    public class BaseItem : BaseModel
    {   
        public string Description { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
    }
}
