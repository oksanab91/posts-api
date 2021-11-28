using System;
using System.Collections.Generic;

namespace PostsAPI.Models
{
    public class PostDTO
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string Author { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }

        public PostDTO() { }
        public PostDTO(Post postItem) =>
            (Id, Header, Author, Details, Date) = (postItem.Id, postItem.Header, postItem.Author, postItem.Details, postItem.PostDate);
    }

    public class PostsDTO
    {
        public IList<PostDTO> PostsList { get; set; } = new List<PostDTO>();
    }
}
