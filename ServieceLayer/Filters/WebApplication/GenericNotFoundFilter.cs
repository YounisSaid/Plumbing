using CoreLayer.BaseEntities;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;

namespace ServiceLayer.Filters.WebApplication
{
    public class GenericNotFoundFilter<T> : IAsyncActionFilter where T : class, IBaseEntity, new()
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericNotFoundFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // returns id(First Parameter)
            var value = context.ActionArguments.FirstOrDefault().Value;
            if (value == null)
            {
                throw new ClientSideException("The Input is Invalid ,Please Enter Valid input!!");
            }

            int id = (int)value!;
            var entity = await _unitOfWork.GetRepository<T>().GetByIdAsync(id);
            if (entity == null)
            {
                throw new ClientSideException("The Id Doesn't Exsit ,Please Enter Valid Id!!");
            }

            await next.Invoke();
            return;

        }
    }
}
