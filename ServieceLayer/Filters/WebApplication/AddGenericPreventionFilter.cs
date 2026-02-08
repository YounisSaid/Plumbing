using CoreLayer.BaseEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.Filters.WebApplication
{
    public class AddGenericPreventionFilter<T> : IAsyncActionFilter where T : class, IBaseEntity, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _toasty;
        public AddGenericPreventionFilter(IToastNotification toasty, IUnitOfWork unitOfWork)
        {
            _toasty = toasty;
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sectionList = await _unitOfWork.GetRepository<T>().GetAll().ToListAsync();
            var sectionName = typeof(T).Name;
            if (sectionList.Any())
            {
                _toasty.AddErrorToastMessage($"You can't Add New {sectionName} ,You Already Have One!!!", new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });

                context.Result = new RedirectToActionResult($"Get{sectionName}List", sectionName, new { Area = "Admin" });
                return;
            }

            await next.Invoke();
            return;
        }
    }
}
