using System.Text.Json;
using AUA.ProjectName.ApiUi.Utility;
using AUA.ProjectName.ApiUi.Utility.Authorizations;
using AUA.ProjectName.ApiUi.Utility.Security;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.WebUi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.ApiUi.Areas.Work.Controller
{
    [Area(AreasConsts.Work)]
    [WebAuthorize(EUserAccess.Profile)]
    public class OrganizationController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var model = await GetUserOrganizationDtosAsync();
            return View(model.ToList());
        }

        private async Task<OrganizationDto[]?> GetUserOrganizationDtosAsync()
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            var response = await ApiHelper.GetRequestWithTokenAsync(ApiUrlConsts.GetCurrentUserOrganizationList, token);

            var result = ApiHelper.GetResultByResponse(response);

            if (ApiHelper.IsSuccess(response))
            {
                return result.Deserialize<OrganizationDto[]>(ApiHelper.GetJsonDeserializerOptions());
            }

            return Array.Empty<OrganizationDto>();
        }

        public IActionResult Insert()
        {
            var model = new OrganizationDto();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync(OrganizationDto organizationDto)
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            await ApiHelper.PostRequestWithTokenAsync(ApiUrlConsts.OrganizationInsert, token, organizationDto);

            SuccessMessage(MessageConsts.InsertSuccess);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateAsync(int id)
        {
            var organizations = await GetUserOrganizationDtosAsync();

            return View(organizations.First(p => p.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(OrganizationDto organizationDto)
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            await ApiHelper.PostRequestWithTokenAsync(ApiUrlConsts.OrganizationUpdate, token, organizationDto);

            SuccessMessage(MessageConsts.UpdateSuccess);

            return RedirectToAction("Index");
        }

        public async Task<bool> _Delete(int id)
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            var response = await ApiHelper.PostRequestWithTokenAsync(ApiUrlConsts.OrganizationDelete, token, new OrganizationDto() { Id = id });

            SuccessMessage(MessageConsts.DeleteSuccess);

            return true;
        }
    }
}
