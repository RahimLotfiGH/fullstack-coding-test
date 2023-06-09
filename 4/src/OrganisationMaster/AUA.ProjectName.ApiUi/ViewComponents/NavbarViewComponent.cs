using AUA.ProjectName.ApiUi.Utility.Security;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.ApiUi.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userLoggedInVm = SecurityHelper.GetUserLoggedInVm(HttpContext);
            return View("Index", userLoggedInVm);
        }
    }
}
