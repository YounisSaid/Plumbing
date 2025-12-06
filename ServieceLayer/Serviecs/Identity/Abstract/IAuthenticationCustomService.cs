using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Serviecs.Identity.Abstract
{
    public interface IAuthenticationCustomService
    {
        Task CreatePasswordCardentialsAndSend(AppUser user, HttpContext context, string email,IUrlHelper url);
    }
}
