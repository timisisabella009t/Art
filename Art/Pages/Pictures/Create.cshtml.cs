using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Art.Data;
using Art.Models;

namespace Art.Pages.Pictures
{
    public class CreateModel : PictureCategoriesPageModel
    {
        private readonly Art.Data.ArtContext _context;

        public CreateModel(Art.Data.ArtContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            var picture = new Picture();
            picture.PictureCategories = new List<PictureCategory>();
            PopulateAssignedCategoryData(_context, picture);
            return Page();
        }

        [BindProperty]
        public Picture Picture { get; set; }
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newPicture = new Picture();
            if (selectedCategories != null)
            {
                newPicture.PictureCategories = new List<PictureCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new PictureCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newPicture.PictureCategories.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Picture>(
            newPicture,
            "Picture",
            i => i.Title, i => i.Author,
            i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                _context.Picture.Add(newPicture);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newPicture);
            return Page();
        }

    


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Picture.Add(Picture);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
