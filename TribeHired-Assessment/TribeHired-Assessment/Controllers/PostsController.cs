using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribeHired_Assessment.Models;
using TribeHired_Assessment.Repositories;

namespace TribeHired_Assessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        IRepository<Post> _postRepository;
        IRepository<Comment> _commentRepository;
        public PostsController(IRepository<Post> postRepository, IRepository<Comment> commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(long Id)
        {
            var posts = await _postRepository.Get(Id);
            return Ok(posts);
        }

        [HttpGet("")]
        public async Task<IActionResult> QueryTopPosts()
        {
            var posts = await _postRepository.GetAll();
            var comments = await _commentRepository.GetAll();
            var result = posts.GroupJoin(comments,
                post => post.Id, comment => comment.PostId,
                (post, comments) => new
                {
                    post.Id,
                    post.UserId,
                    post.Title,
                    post.Body,
                    CommentCount = comments.Count(),
                    Comments = comments
                })
                .OrderByDescending(O => O.CommentCount);
            return Ok(result);
        }
    }
}
