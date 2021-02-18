using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Art.Data;
using Art.Models;

namespace Art.Pages.Pictures
{
    public class IndexModel : PageModel
    {
        private readonly Art.Data.ArtContext _context;

        public IndexModel(Art.Data.ArtContext context)
        {
            _context = context;
        }

        public IList<Picture> Picture { get;set; }
        public PictureData PictureD { get; set; }
        public int PictureID { get; set; }
        public int CategoryID { get; set; }
        public async Task OnGetAsync(int? id, int? categoryID)
        {
            PictureD = new pictureData();

            PictureD.Pictures = await _context.Picture
            .Include(b => b.Publisher)
            .Include(b => b.PictureCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();
            if (id != null)
            {
                PictureID = id.Value;
                Picture picture = PictureD.Pictures
                .Where(i => i.ID == id.Value).Single();
                PictureD.Categories = picture.PictureCategories.Select(s => s.Category);
            }
        }


        public async Task OnGetAsync()
        {
            Picture = await _context.Picture
                .Include(b => b.Publisher)
                .ToListAsync();
        }
    }
}
