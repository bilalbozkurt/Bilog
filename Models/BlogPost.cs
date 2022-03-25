namespace bilog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Link { get; set; }
        public string FeaturedImage { get; set; }
        public List<Category>? Categories { get; set; }
        public User Author { get; set; }
    }
}