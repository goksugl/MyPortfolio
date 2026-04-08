using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.ViewComponents._LayoutViewComponents
{
    public class _LayoutScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }       
    }
}
