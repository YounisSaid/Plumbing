using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ServiceLayer.Helpers.Identity
{
    public static class ModelErrorHelper
    {
        public static ModelStateDictionary AddModelStateListErrors(this ModelStateDictionary modelState,List<string>errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(string.Empty, error);
            }
            return modelState;
        }

        public static ModelStateDictionary AddModelStateListErrors(this ModelStateDictionary modelState, IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }
            return modelState;
        }
    }
}
