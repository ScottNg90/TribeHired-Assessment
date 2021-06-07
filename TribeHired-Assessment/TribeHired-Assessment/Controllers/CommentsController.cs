using Microsoft.AspNet.OData.Query;
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
    public class CommentsController : ControllerBase
    {
        IRepository<Comment> _commentRepository;
        public CommentsController(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(long Id)
        {
            var posts = await _commentRepository.Get(Id);
            return Ok(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Query(ODataQueryOptions<Comment> query)
        {
            var comments = await _commentRepository.GetAll();
            var result = query.ApplyTo(comments);
            return Ok(result);
        }
    }
}
