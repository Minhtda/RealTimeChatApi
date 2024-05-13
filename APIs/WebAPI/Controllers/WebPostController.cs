using Application.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    public class WebPostController : BaseController
    {
        private readonly IPostService _postService;
        public WebPostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpDelete("{postId}")]
        public async Task<IActionResult> BanPost(Guid postId)
        {
            bool isDelete= await _postService.BanPost(postId);
            if (isDelete)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
