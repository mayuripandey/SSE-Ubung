namespace BookmarkService.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string User { get; set; }
        public string Category { get; set; }
    }
}