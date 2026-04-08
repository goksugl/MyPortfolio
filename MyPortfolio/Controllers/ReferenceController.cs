using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using System.Linq;

namespace MyPortfolio.Controllers
{
    public class ReferenceController : Controller
    {
        MyPortfolioContext context = new MyPortfolioContext();
        public IActionResult ReferenceList()
        {
            var values = context.References.ToList();

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateReference()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult CreateReference(MyPortfolio.DAL.Entities.Reference reference, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var newImageName = Guid.NewGuid() + extension;

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var location = Path.Combine(folderPath, newImageName);

                using (var stream = new FileStream(location, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                reference.ImageUrl = "/images/" + newImageName;
            }

            context.References.Add(reference);
            context.SaveChanges();

            return RedirectToAction("ReferenceList"); 
        }
    }
}
