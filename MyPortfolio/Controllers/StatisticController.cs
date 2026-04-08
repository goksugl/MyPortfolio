    using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.Controllers
{
    public class StatisticController : Controller
    {
        MyPortfolioContext context = new MyPortfolioContext();
        public IActionResult Index()
        {
            ViewBag.v1 = context.Skills.Count();
            ViewBag.v2= context.Messages.Count();
            ViewBag.v3 = context.Messages.Where(x => x.IsRead == false).Count();
            ViewBag.v4 = context.Messages.Where(x => x.IsRead == true).Count();

            var skills = context.Skills.ToList();
            ViewBag.SkillTitles = skills.Select(x => x.Title).ToList(); 
            ViewBag.SkillValues = skills.Select(x => x.Value).ToList(); 
            return View();
        }
    }
}
