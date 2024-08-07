using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer.Interfaces
{
    public interface IbaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetALLAsync(params Expression<Func<T,object>> []Includes  );
        Task<IEnumerable<T>> GetALLAsync( );
        Task<T>GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task<T>AddAysnc(T item);
        void Delete(T item);
        T Update(T entity);
         IEnumerable<T>  PaginationSearch<Tkey>(Expression<Func<T,bool>> quary, Expression<Func<T,Tkey>> criteria,int pagesize = 10, int pagenumber = 0);
        IEnumerable<T> Filter(Expression<Func<T, bool>> match, int take = 0, int skip = 0);
    }
}
