using Domain_or_abstractionLayer.Entites;
using Domain_or_abstractionLayer.Extenstion;
using Domain_or_abstractionLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace stocks_webproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PortofolioController : ControllerBase

    {
        private IportofolioRepository portoflioRepo;
        private UserManager<APPUser> UserManager;
        private IStockRepository stockRepository;


        public PortofolioController(IportofolioRepository portoflioRepo, UserManager<APPUser> userManager, IStockRepository stockRepository)
        {
            this.portoflioRepo = portoflioRepo;
            this.UserManager = userManager;
            this.stockRepository = stockRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var user = await UserManager.FindByNameAsync(username);
            var result = await portoflioRepo.GetUserPortfolio(user);
            if (result == null)
            {
                return NotFound(" the item is not found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortofolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await UserManager.FindByNameAsync(username);
            var stock = await stockRepository.GetBySymbolAsync(symbol);
           
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await stockRepository.CreateAsync(stock);
                }

            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await portoflioRepo.GetUserPortfolio(user);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new Portfolio
            {
                SockId = stock.StockId,
                AppUserId =user.Id  
            };

            await portoflioRepo.CreateAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        } 
        [HttpDelete]
        public async Task<IActionResult> DeletePortofolio( string symbol)
        {
            var username = User.GetUsername();
            var appUser = await UserManager.FindByNameAsync(username);

            var userPortfolio = await portoflioRepo.GetUserPortfolio(appUser);

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 1)
            {
                await portoflioRepo.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }

            return Ok();
        }
    }
    }

