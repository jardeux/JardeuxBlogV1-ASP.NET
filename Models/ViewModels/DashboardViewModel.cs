namespace JardeuxBlogV1.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalBlogCount { get; set; }
        public int TotalViewCount { get; set; }
        public Blog MostViewBlog { get; set; }
        public Blog LastPublishBlog { get; set; }
        public int TotalCommentCount { get; set; }
        public Blog MostCommentedBlog { get; set; }
        public int TodayCommentCount { get; set; }  
    }
}
