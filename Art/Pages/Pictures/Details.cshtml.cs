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
    public class DetailsModel : PageModel
    {
        private readonly Art.Data.ArtContext _context;

        public DetailsModel(Art.Data.ArtContext context)
        {
            _context = context;
        }

        public Picture Picture { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Picture = await _context.Picture.FirstOrDefaultAsync(m => m.ID == id);

            if (Picture == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
