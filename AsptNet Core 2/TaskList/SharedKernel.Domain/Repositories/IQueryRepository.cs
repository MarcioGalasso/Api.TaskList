using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Modelo.Interface;

namespace SharedKernel.Domain.Repositories
{
    public interface IQueryRepository<T> where T : IEntity
    {
        T Get(long id, bool loadFirstChild = false);        
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool loadFirstChild = false);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] include);
        IQueryable<T> GetAll(bool loadFirstChild = false);
        
    }
}