namespace Tecnicell.Server.Models.Entity
{
    public class WorkType
    {
        public string Name { get; set; }
        public ICollection<DiaryWork> DiaryWorks { get; set; }
    }
}
