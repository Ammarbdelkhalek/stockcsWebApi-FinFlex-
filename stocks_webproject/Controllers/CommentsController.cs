using BusinessLogic_Layer.data;
using Domain_or_abstractionLayer.helper;
using Domain_or_abstractionLayer.Interface;
using Domain_or_abstractionLayer.modelsDTO.Comment_DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stocks_webproject.Mapper.comments;

namespace stocks_webproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController(ICommentRepository commentRepo ,IStockRepository stockRepo ,ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetALLAsync([FromQuery]CommentQuaryObiect quary)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            var comments = await commentRepo.GetAllAsync(quary);
            var commentModel = comments.Select(x=>x.ToCommentDTO()).ToList();
            return Ok(commentModel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var comment = await commentRepo.GetByIdAsync(id);
            if(comment == null)
            {
                return BadRequest("Item Is Not Exist ");
            }
            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateAsync([FromQuery] int stockId,[FromBody]CreateCommentDTO commentDTO )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await stockRepo.IsExist(stockId)==false)
            {
                return BadRequest("stock is not exist");
            }

            var commentModel = commentDTO.ToCommentFromCreate(stockId);
            await commentRepo.AddAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.CommentId }, commentModel.ToCommentDTO());
        }

        [HttpPut]

        public async Task<IActionResult>UpdateAsync(int id,UpdateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var commentModel = await commentRepo.UpdateAsync(id, commentDTO);
            if(commentModel == null)
            {
                return BadRequest(" Item Not Found");
            }
            return Ok(commentModel.ToCommentDTO());
        }

        [HttpDelete]
        public  async Task<IActionResult>DeleteAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var commetnModel = await commentRepo.DeleteAsync(id);
            if(commetnModel == null)
            {
                return BadRequest();
            }
            return Ok("Deleted Successfully");
      }
    }
}
