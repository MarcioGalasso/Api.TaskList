using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using SharedKernel.Domain.Enum;
using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Services.Interface;
using SharedKernel.Domain.Modelo.Interface;

namespace SharedKernel.Domain.Services
{
    public class QueryService<T> : IQueryService<T> where T : EntityBase, IEntity
    {         
        public IQueryRepository<T> Repository { get; }
        protected IConfiguration Configuration;
        protected IRestService RestService;

        public QueryService(IQueryRepository<T> repository, IConfiguration configuration, IRestService restService)
        {
            Repository = repository;
            this.Configuration = configuration;
            this.RestService = restService;
        }

        public virtual T Get(long id, bool loadFirstChild = false)
        {
            try
            {
                var entity = Repository.Get(id, loadFirstChild);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<T> GetAll(bool loadFirstChild = false)
        {
            try
            {
                var entities = Repository.GetAll(loadFirstChild).ToList();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> @where, bool loadFirstChild = false)
        {
            try
            {
                var entities = Repository.Get(@where, loadFirstChild).ToList();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual PageResult<T> GetPaged(int page, PageSize size, Expression<Func<T, bool>> where)
        {
            if (page < 1)
                throw new ValidationException("Número da Página começa em 1");

            try
            {
                var result = new PageResult<T> { Page = page };
              

                var query = Repository.Get(where, true);

                result.TotalItems = query.Count();
                result.PageSize = (int)size;
                result.TotalPages =
                    (int)Math.Ceiling((double)result.TotalItems / (int)size);

                result.Data = query
                    .Skip((int)size * --page)
                    .Take((int)size)
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public IList<T> GetAll(Expression<Func<T, bool>> where, params string[] include)
        {
            try
            {
                var entities = Repository.Get(@where, include).ToList();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}