using Domain_or_abstractionLayer.Entites;
using Domain_or_abstractionLayer.modelsDTO.Comment_DTO;
using stocks_webproject.models.Comment_DTO;

namespace stocks_webproject.Mapper.comments
{
    public static class commentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment commentModel)
        {
            return new CommentDTO
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                Id = commentModel.CommentId,
                StockId = commentModel.StockId,
            };

        }
        public static Comment ToCommentFromCreate(this CreateCommentDTO commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentDTO commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                
            };
        }

    }
}
