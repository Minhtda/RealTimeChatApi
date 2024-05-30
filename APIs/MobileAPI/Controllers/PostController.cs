using Application.InterfaceService;
using Application.ViewModel.ProductModel;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MobileAPI.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            List<Post> posts = await _postService.GetAllPost();
            return Ok(posts);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            bool isCreate = await _postService.CreatePost(post);
            if (isCreate)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            bool isUpdated = await _postService.UpdatePost(post);
            if (isUpdated)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(Guid postId)
        {
            bool isRemoved = await _postService.DeletePost(postId);
            if (isRemoved)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
