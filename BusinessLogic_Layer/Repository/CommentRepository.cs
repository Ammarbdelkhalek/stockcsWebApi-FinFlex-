using BusinessLogic_Layer.data;
using Domain_or_abstractionLayer.Entites;
using Domain_or_abstractionLayer.helper;
using Domain_or_abstractionLayer.Interface;
using Domain_or_abstractionLayer.modelsDTO.Comment_DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_Layer.Repository
{
    public class CommentRepository(ApplicationDbContext context) : ICommentRepository
    {
        public async Task<IEnumerable<Comment>> GetAllAsync(CommentQuaryObiect quary)
        {
           IQueryable<Comment> result = context.Set<Comment>();
            if (!string.IsNullOrWhiteSpace(quary.Symbol))
            {
                result = result.Where(x => x.Stock.Symbol == quary.Symbol);
            }
            result = quary.IsDecsending == true ? result.OrderByDescending(x => x.CreatedOn) : result.OrderBy(x => x.CreatedOn);
                
            return await result.ToArrayAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var quary =   await context.Set<Comment>().FindAsync(id);
            if( quary == null)
            {
               return null;
            }
            else
            return quary;
        }

        public async Task<Comment> AddAsync(Comment entity)
        {
            await context.Set<Comment>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public  async Task<Comment> UpdateAsync(int id, UpdateCommentDTO entity)
        {
            var comment = context.Comments.FirstOrDefault(x => x.CommentId == id);
            if (comment == null)
            {
                return null;
            }
            comment.Title = entity.Title;
            comment.Content = entity.Content;

            context.Set<Comment>().Update(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            var commentModel =  await context.Comments.FindAsync(id);
            if( commentModel == null )
            {
                return null;
            }
             context.Comments.Remove(commentModel);
            await context.SaveChangesAsync();
            return commentModel;    
        }

        
    }
}
