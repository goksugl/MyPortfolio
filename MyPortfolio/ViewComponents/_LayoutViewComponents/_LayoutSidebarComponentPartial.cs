using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.ViewComponents._LayoutViewComponents
{
    public class _LayoutSidebarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }       
    }
}
