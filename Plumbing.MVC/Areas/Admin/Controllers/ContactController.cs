using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Contact;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminObserver")]
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IValidator<ContactAddMV> _categoryAddValidator;
        private readonly IValidator<ContactUpdateMV> _categoryUpdateValidator;
        public ContactController(IContactService contactService, IValidator<ContactAddMV> categoryAddValidator, IValidator<ContactUpdateMV> categoryUpdateValidator)
        {
            _contactService = contactService;
            _categoryAddValidator = categoryAddValidator;
            _categoryUpdateValidator = categoryUpdateValidator;
        }

        public async Task<IActionResult> GetContactList()
        {
            var contactList = await _contactService.GetAllListAsync();
            return View(contactList);
        }

        [HttpGet]
        [ServiceFilter(typeof(AddGenericPreventionFilter<Contact>))]
        public async Task<IActionResult> AddContact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(ContactAddMV model)
        {
            var ValidationResult = await _categoryAddValidator.ValidateAsync(model);
            if (ValidationResult.IsValid)
            {
                await _contactService.AddContactAsync(model);
                return RedirectToAction(nameof(GetContactList), "Contact", new { Area = "Admin" });

            }
            ValidationResult.AddToModelState(ModelState);
            return View(model);
        }

        [HttpGet]
        [ServiceFilter(typeof(GenericNotFoundFilter<Contact>))]

        public async Task<IActionResult> UpdateContact(int Id)
        {
            var contact = await _contactService.GetByIdAsync(Id);

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(ContactUpdateMV model)
        {
            var ValidationResult = await _categoryUpdateValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
            await _contactService.UpdateContactAsync(model);
            return RedirectToAction(nameof(GetContactList), "Contact", new { Area = "Admin" });
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteContact(int Id)
        {
            await _contactService.DeleteContactAsync(Id);
            return RedirectToAction(nameof(GetContactList), "Contact", new { Area = "Admin" });
        }
    }
}