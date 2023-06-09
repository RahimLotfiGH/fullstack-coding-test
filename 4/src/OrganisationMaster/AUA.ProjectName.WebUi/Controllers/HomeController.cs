using Microsoft.AspNetCore.Mvc;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Models.ViewModels.BaseViewModel;

namespace AUA.ProjectName.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ErrorPage(int resultStatus)
        {
            var model = CreateErrorPageVm(resultStatus);
            return View(model);
        }

        private ErrorPageVm CreateErrorPageVm(int errorCode)
        {
            return new ErrorPageVm()
            {
                ErrorCode = errorCode,
                Message = ((EResultStatus)errorCode).ToDescription()
            };
        }
    }
}