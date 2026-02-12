using EntityLayer.Identity.Entites;
using EntityLayer.WebApplication.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public DashboardService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<int> GetCategoriesCountAsync()
        {
            return await _unitOfWork.GetRepository<Category>().GetAllCountAsync();
        }

        public async Task<int> GetPortofliosCountAsync()
        {
            return await _unitOfWork.GetRepository<Portfolio>().GetAllCountAsync();
        }

        public async Task<int> GetServicesCountAsync()
        {
            return await _unitOfWork.GetRepository<Service>().GetAllCountAsync();
        }

        public async Task<int> GetTeamsCountAsync()
        {
            return await _unitOfWork.GetRepository<Team>().GetAllCountAsync();
        }

        public async Task<int> GetTestimonialCountAsync()
        {
            return await _unitOfWork.GetRepository<Testimonial>().GetAllCountAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _userManager.Users.CountAsync();
        }
    }
}
