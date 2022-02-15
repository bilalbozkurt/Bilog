namespace bilog.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public List<BlogPost>? BlogPosts { get; set; }
        public string Link { get; set; }
    }
}