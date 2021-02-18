using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Art.Data;
using Art.Models;

namespace Art.Pages.Pictures
{
    public class EditModel : PictureCategoriesPageModel
    {
        private readonly Art.Data.ArtContext _context;

        public EditModel(Art.Data.ArtContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Picture Picture { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Picture = await _context.Picture
                .Include(b => b.Publisher)
               .Include(b => b.PictureCategories).ThenInclude(b => b.Category)
                  .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Picture == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Picture);
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
            selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pictureToUpdate = await _context.Picture
            .Include(i => i.Publisher)
            .Include(i => i.PictureCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (pictureToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Picture>(
            pictureToUpdate,
            "Picture",
            i => i.Title, i => i.Author,
            i => i.Price, i => i.PublishingDate, i => i.Publisher))
            {
                UpdatePictureCategories(_context, selectedCategories, pictureToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
           
            UpdatePictureCategories(_context, selectedCategories, pictureToUpdate);
            PopulateAssignedCategoryData(_context, pictureToUpdate);
            return Page();
        }
    }
}
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
        if (!PictureExists(Picture.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

       private bool PictureExists(int id)
        {
            return _context.Picture.Any(e => e.ID == id);
        }
    }
}
