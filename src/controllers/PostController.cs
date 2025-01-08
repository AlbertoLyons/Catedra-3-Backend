using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_3_Backend.src.dtos.post;
using Catedra_3_Backend.src.helpers;
using Catedra_3_Backend.src.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catedra_3_Backend.src.controllers
{
    [Route("api/posts")]
    [Authorize]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        JwtValidator jwtValidator = new JwtValidator();

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            if (jwtValidator.IsTokenExpired(Request.Headers["Authorization"]!))
            {
                return Unauthorized(new { Message = "Token expired" });
            }
            try
            {
                var posts = await _postRepository.GetPosts();
                return Ok(posts);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDTO createPost)
        {
            if (jwtValidator.IsTokenExpired(Request.Headers["Authorization"]!))
            {
                return Unauthorized(new { Message = "Token expired" });
            }
            try
            {
                var post = await _postRepository.CreatePost(createPost);
                var response = new
                {
                    Message = "Post created",
                    Post = post
                };
                return Ok(post);
            } catch (Exception e)
            {
                if (e.Message.Contains("Image is required") || e.Message.Contains("Image must be a PNG or JPG") || e.Message.Contains("User not found"))
                {
                    return BadRequest(new { e.Message });
                }
                return BadRequest(e.Message);
            }

        }
    }
}