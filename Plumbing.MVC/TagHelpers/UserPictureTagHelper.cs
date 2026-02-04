using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Plumbing.MVC.TagHelpers
{
    public class UserPictureTagHelper : TagHelper
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        
        public UserPictureTagHelper(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;            
        }

        public string? FileName { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img"; 
            string SignedInUserId = _signInManager.Context.User.Claims.First(c=>c.Type.Contains("identifier")).Value;

            var user = await _userManager.FindByIdAsync(SignedInUserId);

            if(!string.IsNullOrEmpty(user!.FileName))
            {
                output.Attributes.SetAttribute("src", $"/images/{FileName}");
                

            }
            else
            {

                output.Attributes.SetAttribute("src", $"/images/default.png");
                
            }
        }
               

    }
}
