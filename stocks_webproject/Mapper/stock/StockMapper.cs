using Domain_or_abstractionLayer.Entites;
using stocks_webproject.Mapper.comments;
using stocks_webproject.models.Stock_DTO;

namespace stocks_webproject.Mapper.stock
{
    public static class StockMapper
    {
        public static StockDTO ToStockDto(this Stock stockModel)
        {
            return new StockDTO
            {
                Id = stockModel.StockId,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                purchase = stockModel.purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                marketCap = stockModel.marketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDTO()).ToList()
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                purchase = stockDto.purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                marketCap = stockDto.marketCap
            };
        }

    }
}
