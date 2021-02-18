using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Art.Models
{
    public class PictureData
    {
        public IEnumerable<Picture> Picture { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<PictureCategory> PictureCategories { get; set; }
    }
}
