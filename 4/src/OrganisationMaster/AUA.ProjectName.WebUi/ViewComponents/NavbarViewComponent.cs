using AUA.ProjectName.Models.DataModels.LoginDataModels;
using AUA.ProjectName.WebUi.Utility.Security;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.WebUi.ViewComponents
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
