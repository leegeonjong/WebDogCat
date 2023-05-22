namespace dogcat.Models.ViewModels
{
    public class EditWriteRequest
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string? Context { get; set; }
        public int ViewCnt { get; set; }
        public string Category { get; set; }
        public DateTime Time {get; set; }
    }
}
