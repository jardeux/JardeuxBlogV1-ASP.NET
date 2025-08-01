namespace JardeuxBlogV1.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime PublishDate { get; set; }
        public int BlogId { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }




    }
}
