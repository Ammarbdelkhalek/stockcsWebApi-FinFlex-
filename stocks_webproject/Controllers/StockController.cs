using BusinessLogic_Layer.data;
using Domain_or_abstractionLayer.helper;
using Domain_or_abstractionLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stocks_webproject.Mapper.stock;
using stocks_webproject.models.Stock_DTO;

namespace stocks_webproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController(ApplicationDbContext context , IStockRepository stockrepo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]QuaryObject quary)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stocks = await stockrepo.GetAllAsync(quary);
            var stockDto = stocks.Select(x => x.ToStockDto()).ToList();
            return Ok(stockDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByIDAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock  = await stockrepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound("Itme IsNOT exist ");
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> AddStock( CreateStockDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateDTO();
            await stockrepo.CreateAsync(stockModel);

            return Ok(stockDto);

        }
        [HttpPut]
        public async Task<IActionResult>UpdateAsync(int id , UpdateStockDTO UpdateStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockmodel = await stockrepo.UpdateAsync(id,UpdateStock);
            if (stockmodel == null)
            {
                return NotFound();
            }
            return Ok(UpdateStock);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync (int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(); 
            }
            stockrepo.DeleteAsync(id);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("IsExist{id:int}")]
        public async Task<IActionResult> IsExist(int id)
        {
             return  Ok(await stockrepo.IsExist(id));
        }
    }
}
