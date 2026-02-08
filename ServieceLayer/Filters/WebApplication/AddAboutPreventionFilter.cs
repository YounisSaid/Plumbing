using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NToastNotify;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Filters.WebApplication
{
    public class AddAboutPreventionFilter : IAsyncActionFilter
    {
        private readonly IAboutService _aboutService;
        private readonly IToastNotification _toasty;
        public AddAboutPreventionFilter(IAboutService aboutService, IToastNotification toasty)
        {
            _aboutService = aboutService;
            _toasty = toasty;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var abouts = await _aboutService.GetAllListAsync();
            if (abouts.Any())
            {
                _toasty.AddErrorToastMessage("You can't Add New About ,You Already Have One!!!", new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });

                context.Result = new RedirectToActionResult("GetAboutList", "About", new { Area = "Admin" });
                return;
            }

            await next.Invoke();
            return;
        }
    }
}
