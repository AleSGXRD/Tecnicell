namespace Tecnicell.Server.Models.Entity
{
    public class DiaryWork
    {
        public DateTime Date { get; set; }
        public string WorkType { get; set; } = null!;
        public string? Description { get; set; }
        public string? SaleCode { get; set; }
        public string? UserCode { get; set; }
        public WorkType? WorkTypeNavigation { get; set; }
        public Sale? SaleCodeNavigation { get; set; }
        public UserInfo? UserCodeNavigation { get; set; }
    }
}
