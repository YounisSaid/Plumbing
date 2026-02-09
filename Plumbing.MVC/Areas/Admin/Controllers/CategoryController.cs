using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Category;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryAddMV> _categoryAddValidator;
        private readonly IValidator<CategoryUpdateMV> _categoryUpdateValidator;
        public CategoryController(ICategoryService categoryService, IValidator<CategoryAddMV> categoryAddValidator, IValidator<CategoryUpdateMV> categoryUpdateValidator)
        {
            _categoryService = categoryService;
            _categoryAddValidator = categoryAddValidator;
            _categoryUpdateValidator = categoryUpdateValidator;
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
            var ValidationResult = await _categoryAddValidator.ValidateAsync(model);
            if (ValidationResult.IsValid)
            {
                await _categoryService.AddCategoryAsync(model);
                return RedirectToAction(nameof(GetCategoryList), "Category", new { Area = "Admin" });
            }
            ValidationResult.AddToModelState(ModelState);
            return View(model);
        }

        [HttpGet]
        [ServiceFilter(typeof(GenericNotFoundFilter<Category>))]

        public async Task<IActionResult> UpdateCategory(int Id)
        {
            var category = await _categoryService.GetByIdAsync(Id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateMV model)
        {
            var ValidationResult = await _categoryUpdateValidator.ValidateAsync(model);
            if (ValidationResult.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(model);
                return RedirectToAction(nameof(GetCategoryList), "Category", new { Area = "Admin" });
            }
            ValidationResult.AddToModelState(ModelState);
            return View(model);

        }

        public async Task<IActionResult> DeleteCategory(int Id)
        {
            await _categoryService.DeleteCategoryAsync(Id);
            return RedirectToAction(nameof(GetCategoryList), "Category", new { Area = "Admin" });
        }
    }
}
