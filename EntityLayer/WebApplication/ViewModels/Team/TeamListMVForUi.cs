namespace EntityLayer.WebApplication.ViewModels.Team
{
    public class TeamListMVForUi
    {
        public string FullName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string FileName { get; set; } = null!;

        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }
    }
}
