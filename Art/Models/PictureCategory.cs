using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Art.Models
{
    public class PictureCategory
    {
        public int ID { get; set; }
        public int PictureID { get; set; }
        public Picture Picture { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
