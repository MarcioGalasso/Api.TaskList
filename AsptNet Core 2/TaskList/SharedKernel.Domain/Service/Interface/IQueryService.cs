using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SharedKernel.Domain.Enum;
using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Modelo.Interface;

namespace SharedKernel.Domain.Services.Interface
{
    public interface IQueryService<T> where T : IEntity
    {
        T Get(long id, bool loadFirstChild = false);
        IList<T> GetAll(bool loadFirstChild = false);
        IList<T> GetAll(Expression<Func<T, bool>> where, bool loadFirstChild = false);
        IList<T> GetAll(Expression<Func<T, bool>> where, params string[] include);
        PageResult<T> GetPaged(int page, PageSize size, Expression<Func<T, bool>> where);
    }
}