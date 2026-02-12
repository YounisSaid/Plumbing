using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var uiCategoryList = await _categoryService.GetAllListForUiAsync();
            return View(uiCategoryList);
        }
    }
}
