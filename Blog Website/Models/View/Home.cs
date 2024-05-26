using Blog_Website.Models.Domain;

namespace Blog_Website.Models.View
{
    public class Home
    {
        public IEnumerable<BlogPost> Posts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
