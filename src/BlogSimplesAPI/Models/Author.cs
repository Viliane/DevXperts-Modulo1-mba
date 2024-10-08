namespace BlogSimplesAPI.Models
{
    public class Author
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
