using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EVTovar.Models
{
    public abstract class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
