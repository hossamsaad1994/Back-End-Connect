using connect_.Dto;
using connect_.Models;
using connect_.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace connect_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsServices _postServices;

        public PostsController(IPostsServices postServices)
        {
            _postServices = postServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var post = await _postServices.GetAll();
            return Ok(post);
        }
        [HttpPost]
        public async Task<IActionResult> CreateASync(PostsDto dto)
        {
            var post = new Post
            {
                Content = dto.Content,
                CreatedDate = DateTime.UtcNow,
                Title = dto.Title,
                UserId = dto.UserId,
            };
            await _postServices.Add(post);
            return Ok(post);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PostsDto dto)
        {
            var post = await _postServices.GetById(id);
            if (post == null)
                return NotFound($"no Posts was found with : {id}");
            post.UserId = dto.UserId;
            _postServices.Update(post);
            return Ok(post);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var post = await _postServices.GetById(id);
            if (post == null)
                return NotFound($"no Posts was found with : {id}");
            _postServices.Delete(post);
            return Ok(post);
        }
    }
}
