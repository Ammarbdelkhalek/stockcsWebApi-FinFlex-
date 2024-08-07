using BusinessLogic_Layer.data;
using Domain_or_abstractionLayer.Entites;
using Domain_or_abstractionLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_Layer.Repository
{
    public class PortofolioRepository(ApplicationDbContext context) : IportofolioRepository
    {
        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await context.portfolios.AddAsync(portfolio);
            await context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(APPUser appUser, string symbol)
        {
            var portfolioModel = await context.portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if (portfolioModel == null)
            {
                return null;
            }

            context.portfolios.Remove(portfolioModel);
            await context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolio(APPUser user)
        {
            return await context.portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                StockId = stock.SockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                purchase = stock.Stock.purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                marketCap = stock.Stock.marketCap
            }).ToListAsync();
        }


    }
}
