using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutList()
        {
            MyPortfolioContext context = new MyPortfolioContext();
            var values = context.Abouts.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            MyPortfolioContext context = new MyPortfolioContext();
            var value = context.Abouts.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateAbout(About about)
        {
            MyPortfolioContext context = new MyPortfolioContext();
            var value = context.Abouts.Find(about.AboutId);
            value.Title = about.Title;
            value.SubDescription = about.SubDescription;
            value.Details = about.Details;
            context.SaveChanges();
            return RedirectToAction("AboutList");
        }


    }
}
