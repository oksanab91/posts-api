using System;
using System.Collections.Generic;

namespace PostsAPI.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string Author { get; set; }
        public string Details { get; set; }
        public DateTime PostDate { get; set; }
    }

    public class Posts
    {
        public IList<Post> PostsList { get; set; } = new List<Post>();
    }
}
