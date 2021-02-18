using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Art.Data;


namespace Art.Models
{
    public class PictureCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(ArtContext context,
        Picture picture)
        {
            var allCategories = context.Category;
            var pictureCategories = new HashSet<int>(
            picture.PictureCategories.Select(c => c.PictureID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = pictureCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdatePictureCategories(ArtContext context,
        string[] selectedCategories, Picture pictureToUpdate)
        {
            if (selectedCategories == null)
            {
                pictureToUpdate.PictureCategories = new List<PictureCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var pictureCategories = new HashSet<int>
            (pictureToUpdate.PictureCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!pictureCategories.Contains(cat.ID))
                    {
                        pictureToUpdate.PictureCategories.Add(
                        new PictureCategory
                        {
                            PictureID = pictureToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (pictureCategories.Contains(cat.ID))
                    {
                        PictureCategory courseToRemove
                        = pictureToUpdate
                        .PictureCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
