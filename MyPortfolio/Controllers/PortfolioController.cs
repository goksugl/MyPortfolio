using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using System.IO;

namespace MyPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        MyPortfolioContext context = new MyPortfolioContext();
        public IActionResult PortfolioList()
        {
            var values = context.Portfolios.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreatePortfolio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePortfolio(MyPortfolio.DAL.Entities.Portfolio portfolio, IFormFile imageFile)
        {
            // Kullanıcı resim seçmiş mi kontrol ediyoruz
            if (imageFile != null && imageFile.Length > 0)
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var newImageName = Guid.NewGuid() + extension;

                // Resmi kaydedeceğimiz klasör yolunu belirliyoruz
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");

                // EĞER BÖYLE BİR KLASÖR YOKSA, ÇÖKME! OTOMATİK OLUŞTUR:
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var location = Path.Combine(folderPath, newImageName);

                using (var stream = new FileStream(location, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                portfolio.ImageUrl = "/images/" + newImageName;
            }

            // Dosya işlemleri bittikten sonra projeyi kaydediyoruz
            context.Portfolios.Add(portfolio);
            context.SaveChanges();

            return RedirectToAction("PortfolioList");
        }


        public IActionResult DeletePortfolio(int id)
        {
            var value = context.Portfolios.Find(id);
            context.Portfolios.Remove(value);
            context.SaveChanges();
            return RedirectToAction("PortfolioList");
        }


        [HttpGet]
        public IActionResult UpdatePortfolio(int id)
        {
            var value = context.Portfolios.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdatePortfolio(MyPortfolio.DAL.Entities.Portfolio portfolio)
        {

            var value = context.Portfolios.Find(portfolio.PortfolioId);

            value.Title = portfolio.Title;
            value.SubTitle = portfolio.SubTitle;
            value.ImageUrl = portfolio.ImageUrl;
            value.Url = portfolio.Url;
            value.Description = portfolio.Description;

            context.SaveChanges();
            return RedirectToAction("PortfolioList");
        }
    }
}
