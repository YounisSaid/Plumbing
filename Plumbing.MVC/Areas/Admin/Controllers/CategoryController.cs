using EntityLayer.WebApp.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using ServieceLayer.Serviecs.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> GetCategoryList()
        {
            var categoryList = await _categoryService.GetAllListAsync();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddMV model)
        {

            await _categoryService.AddCategoryAsync(model);
            return RedirectToAction(nameof(GetCategoryList), "Category", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int Id)
        {
            var category = await _categoryService.GetByIdAsync(Id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateMV model)
        {
            await _categoryService.UpdateCategoryAsync(model);
            return RedirectToAction(nameof(GetCategoryList), "Category", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteCategory(int Id)
        {
            await _categoryService.DeleteCategoryAsync(Id);
            return RedirectToAction(nameof(GetCategoryList), "Category", new { Area = "Admin" });
        }
    }
}
