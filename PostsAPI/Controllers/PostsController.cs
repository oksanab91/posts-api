using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PostsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostsAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private static List<Post> postsData = new List<Post>();

        public PostsController()
        {
            FillPostsData();
        }

        // GET: api/<PostsController/name/1>
        [HttpGet("{authorName}/{pageNumber}")]
        public IEnumerable<PostDTO> GetFiltered(string authorName, int? pageNumber)
        {
            return ReadPostsData(pageNumber ?? 1, authorName);
        }

        // GET: api/<PostsController/1>
        [HttpGet("{pageNumber}")]
        public IEnumerable<PostDTO> GetByPage(int? pageNumber)
        {
            return ReadPostsData(pageNumber ?? 1);
        }

        // GET: api/<PostsController>
        [HttpGet]
        public IEnumerable<PostDTO> Get()
        {
            return ReadPostsData(1);
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            int ind = postsData.FindIndex(post => post.Id == id.ToString());

            if (ind < 0)
            {
                return NotFound();
            }
            
            postsData.RemoveAt(ind);

            return Ok(postsData);
        }

        private static void FillPostsData()
        {
            var rng = new Random();
            postsData = Enumerable.Range(1, 500).Select(index => new Post
            {
                Id = index.ToString(),
                Author = "Author " + index.ToString(),
                Header = "Post head " + index.ToString(),
                PostDate = DateTime.Now.AddDays(index),
                Details = "Post massage " + index.ToString()
            })
            .ToList<Post>();
        }

        private static IList<PostDTO> ReadPostsData(int pageNumber, string authorName = "")
        {
            int pageSize = 10;
            int count = pageNumber * pageSize;
            IEnumerable<Post> filteredPosts = null;            

            if ((authorName ?? "").Length != 0)
            {
                filteredPosts = postsData.Where(post => post.Author.ToUpper().Contains(authorName.ToUpper()))?.Take(count);
            } else
            {
                filteredPosts = postsData.Take(count);
            }
            
            if (filteredPosts != null)
            {
                return filteredPosts.Select(post => new PostDTO(post)).ToList();
            }

            return new List<PostDTO>();
        }
    }
}
