using EntityLayer.WebApplication.ViewModels.Contact;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IValidator<ContactAddMV> _categoryAddValidator;
        private readonly IValidator<ContactUpdateMV> _categoryUpdateValidator;
        public ContactController(IContactService contactService,IValidator<ContactAddMV> categoryAddValidator,IValidator<ContactUpdateMV> categoryUpdateValidator)
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


        public async Task<IActionResult> DeleteContact(int Id)
        {
            await _contactService.DeleteContactAsync(Id);
            return RedirectToAction(nameof(GetContactList), "Contact", new { Area = "Admin" });
        }
    }
}