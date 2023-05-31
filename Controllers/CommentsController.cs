using connect_.Dto;
using connect_.Models;
using connect_.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace connect_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsSevices _commentsService;

        public CommentsController(ICommentsSevices commentsSevices)
        {
            _commentsService = commentsSevices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentsService.GetAll();
            return Ok(comments);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CommentsDto dto)
        {
            var comment = new Comments
            {
                Content = dto.Content,
                PostId = dto.PostId,
                UserId = dto.UserId,
                CreatedDate = dto.CreatedDate,
            };
            await _commentsService.Add(comment);
            return Ok(comment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CommentsDto dto)
        {
            var comment = await _commentsService.GetById(id);
            if (comment == null)
                return NotFound($"no Comments was found with : {id}");
            comment.UserId = dto.UserId;
            _commentsService.Update(comment);
            return Ok(comment);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var comment = await _commentsService.GetById(id);
            if (comment == null)
                return NotFound($"no Comments was found with : {id}");
            _commentsService.Delete(comment);
            return Ok(comment);
        }
    }
}
