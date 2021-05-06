using AutoMapper;
using DotNetWebAPIMVPStarter.Models.Blog;
using DotNetWebAPIMVPStarter.Models.Blog.RequestDtos;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Models.Role;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;
        IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("create_post")]
        public IActionResult CreatePost([FromBody] CreatePostRequest request)
        {
            Post PostMap = _mapper.Map<Post>(request);
            _postService.AddPost(PostMap);
            return Ok();
        }

        [HttpGet("get_all_posts")]
        public IActionResult GetAllPosts([FromQuery] PaginationFilter Filter)
        {
            return Ok(_postService.GetAllPosts(Filter));
        }

        [HttpGet("get_unplished_posts_by_author")]
        public IActionResult GetUnpublishedPostsByAuthor(int AuthorId, [FromQuery] PaginationFilter Filter)
        {
            return Ok(_postService.GetUnpublishedPostsByAuthor(AuthorId, Filter));
        }

        [HttpGet("get_published_posts_by_author")]
        public IActionResult GetPublishedPostsByAuthor(int AuthorId, [FromQuery] PaginationFilter Filter)
        {
            return Ok(_postService.GetPublishedPostsByAuthor(AuthorId, Filter));
        }

        [HttpGet("get_posts_by_publish_date")]
        public IActionResult GetPostsByDatePublished(DateTime DatePublished, [FromQuery] PaginationFilter Filter)
        {
            return Ok(_postService.GetPostsByDatePublished(DatePublished, Filter));
        }

        [HttpGet("get_all_posts_by_author")]
        public IActionResult GetPostsByAuthor(int AuthorId, [FromQuery] PaginationFilter Filter)
        {
            return Ok(_postService.GetPostsByAuthor(AuthorId, Filter));
        }

        [HttpGet("get_post_slug")]
        public IActionResult GetPostBySlug(string Slug)
        {
            return Ok(_postService.GetPostBySlug(Slug));
        }

        [HttpGet("get_post_by_id/{PostId}")]
        public IActionResult GetPostById(int PostId)
        {
            return Ok(_postService.GetPostById(PostId));
        }

        [HttpGet("get_archived_posts_by_author")]
        public IActionResult GetArchivedPostsByAuthor(int AuthorId, [FromQuery] PaginationFilter Filter)
        {
            return Ok(_postService.GetArchivedPostsByAuthor(AuthorId, Filter));
        }

        [HttpPut("update_post/{PostId}")]
        public IActionResult UpdatePost(int PostId, Post Post)
        {
            if (PostId != Post.Id) return BadRequest();
            _postService.UpdatePost(PostId, Post);
            return NoContent();
        }

    }
}
