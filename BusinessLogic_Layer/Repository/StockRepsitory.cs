using BusinessLogic_Layer.data;
using Domain_or_abstractionLayer.Entites;
using Domain_or_abstractionLayer.helper;
using Domain_or_abstractionLayer.Interface;
using Microsoft.EntityFrameworkCore;
using stocks_webproject.models.Stock_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_Layer.Repository
{
    public class StockRepository(ApplicationDbContext context) : IStockRepository
    {

        public async Task<IEnumerable<Stock>> GetAllAsync(QuaryObject quary)
        {
            var result = context.Set<Stock>().Include(x=>x.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(quary.CompanyName))
            {
                result = result.Where(s => s.CompanyName.Contains(quary.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(quary.Symbol))
            {
                result = result.Where(x => x.Symbol.Contains(quary.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(quary.SortBy))
            {
                if (quary.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    result = quary.IsDecsending ? result.OrderByDescending(x => x.Symbol) : result.OrderBy(x => x.Symbol);
                }
            }
            var skipNumber = (quary.PageNumber - 1) * quary.PageSize;

            return await result.Skip(skipNumber).Take(quary.PageSize).ToArrayAsync();
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await context.Set<Stock>().Include(x=>x.Comments).FirstOrDefaultAsync(x=>x.StockId == id);
                
        }

        public async Task<Stock> CreateAsync( Stock stockModel)
        {
            await context.Set<Stock>().AddAsync(stockModel);
            await context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockDTO stockDto)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(x => x.StockId == id);
            if (stock == null)
            {
                return null;
            }
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.purchase = stockDto.purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.marketCap = stockDto.marketCap;

            context.Set<Stock>().Update(stock);
            await context.SaveChangesAsync();
            return stock;
        }

        public void DeleteAsync(int id)
        {
            var stock = context.Set<Stock>().Find(id);
            context.Set<Stock>().Remove(stock);
            context.SaveChanges();
        }

        public async Task<bool> IsExist(int id)
        {
            var result = await context.Set<Stock>().AnyAsync(x=>x.StockId ==id);
            return result;

        }

        public async Task<Stock> GetBySymbolAsync(string symbol)
        {
               var stock =  await context.Set<Stock>().Include(x => x.Comments).FirstOrDefaultAsync(x => x.Symbol == symbol);
            if(stock == null)
                return null;

            return stock;
        }
    }
}
