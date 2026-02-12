namespace ServiceLayer.Serviecs.WebApplication.Abstract
{
    public interface IDashboardService
    {
        Task<int> GetCategoriesCountAsync();
        Task<int> GetPortofliosCountAsync();
        Task<int> GetServicesCountAsync();
        Task<int> GetTeamsCountAsync();
        Task<int> GetTestimonialCountAsync();
        Task<int> GetUsersCountAsync();
    }
}
