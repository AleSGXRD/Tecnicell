using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel.DiaryWork
{
    public class DiaryWorkViewModel
    {
        public DateTime Date { get; set; }
        public string WorkType { get; set; } = null!;
        public string? Description { get; set; }
        public string? SaleCode { get; set; }
        public string? UserCode { get; set; }
        public WorkTypeViewModel? WorkTypeNavigation { get; set; }
        public SaleViewModel? SaleCodeNavigation { get; set; }
        public UserInfoViewModel? UserCodeNavigation { get; set; }
    }
}
