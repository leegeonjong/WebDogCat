namespace dogcat.Models.ViewModels
{
    public class addCommentRequest
    {
        public long UserId { get; set; }
        public long WriteId { get; set; }
        public long Id { get; set; }
        public string? Content { get; set; }
        public DateTime  Time { get; set; }
    }
}
