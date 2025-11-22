using EntityLayer.WebApp.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;
using ServieceLayer.Serviecs.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
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
            await _contactService.AddContactAsync(model);
            return RedirectToAction(nameof(GetContactList), "Contact", new { Area = "Admin" });
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