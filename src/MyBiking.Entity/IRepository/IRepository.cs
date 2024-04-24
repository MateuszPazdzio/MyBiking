using MyBiking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Entity.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Delete(T entity);
        void Create(T entity);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);
        Task<T> GetT(Expression<Func<T, bool>> predicate, string? includeProperties = null);
    }
}
