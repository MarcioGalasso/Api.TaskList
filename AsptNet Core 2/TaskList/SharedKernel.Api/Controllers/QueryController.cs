using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using SharedKernel.DependencyInjector;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using Checkin.CheckinExpress.Domain.Extension;
using System.Reflection;
using SharedKernel.Domain.Modelo;
using SharedKernel.Api.AuthorizationFilter;
using SharedKernel.Domain.Services.Interface;
using SharedKernel.Domain.Enum;

namespace SharedKernel.Api.Controllers
{
    [UserAuthorization]
    public class QueryController<T> : Controller where T :  EntityBase
    {
        protected IQueryService<T> Service { get; set; }
        protected IConfiguration Configuration;

        public QueryController(IQueryService<T> service)
        {
            Service = service;
            Configuration = Kernel.Get<IConfiguration>();
        }

        /// <summary>
        /// Retorna uma entidade dado o seu ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade</returns>
        [HttpGet]
        [Route("{id}")]
        public virtual IActionResult Get(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Informe o ID");

                var result = Service.Get(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public virtual Expression<Func<T, bool>> GetExpressionMandatoryPaginate()
        {
            return (ex => !ex.isDeleted);
        }

        private Expression<Func<T, bool>> InterpreterFilterPaginate(PaginateFilter PaginateFilter, Expression<Func<T, bool>> expressionMandatory = null)
        {

            var parameter = Expression.Parameter(typeof(T));
            var expression= Expression.Lambda<Func<T, bool>>(
              GetBinaryExpression(PaginateFilter.Operator, PaginateFilter.Property, PaginateFilter.Value, parameter), parameter);

            PaginateFilter.Filters.ForEach(filter => {

                var expressionTwo = Expression.Lambda<Func<T, bool>>(
                                GetBinaryExpression(filter.Operator, filter.Property, filter.Value, parameter), parameter);

                if (PaginateFilter.Clausule == "and")
                {
                    expression = expression.And(expressionTwo);
                }
                else if (PaginateFilter.Clausule == "or")
                {
                    expression = expression.Or(expressionTwo);
                }

            });
            
            if (expressionMandatory != null)
                expression = expressionMandatory.And(expression);
            
            return expression;
        }

        private BinaryExpression GetBinaryExpression(string Operator, string property,object value, ParameterExpression parameter) {
           
            if (typeof(Int32) == typeof(T).GetProperty(property).PropertyType) {
                value = Convert.ToInt32(value);
            }
            
            switch (Operator) {
                case "==":
                    return Expression.Equal(Expression.MakeMemberAccess(parameter, typeof(T).GetProperty(property)), Expression.Constant(value));
                case "!=":
                    return Expression.NotEqual(Expression.MakeMemberAccess(parameter, typeof(T).GetProperty(property)), Expression.Constant(value));
                case ">":
                    return Expression.GreaterThan(Expression.MakeMemberAccess(parameter, typeof(T).GetProperty(property)), Expression.Constant(value));
                case ">=":
                    return Expression.GreaterThanOrEqual(Expression.MakeMemberAccess(parameter, typeof(T).GetProperty(property)), Expression.Constant(value));
                case "<":
                    return Expression.LessThan(Expression.MakeMemberAccess(parameter, typeof(T).GetProperty(property)), Expression.Constant(value));
                case "<=":
                    return Expression.LessThanOrEqual(Expression.MakeMemberAccess(parameter, typeof(T).GetProperty(property)), Expression.Constant(value));
                default:
                    return Expression.Equal(Expression.MakeMemberAccess(parameter, typeof(T).GetProperty(property)), Expression.Constant(value));
            }

        }

        

        /// <summary>
        /// Retorna uma entidade dado o seu ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade</returns>
        [HttpPost]
        [Route("LoadPaginate")]
        public virtual IActionResult LoadPaginate([FromBody]Paginate paginate)
        {
            try
            {
                var expression = this.GetExpressionMandatoryPaginate();
                if (paginate.PaginateFilter != null) {
                    expression =  InterpreterFilterPaginate(paginate.PaginateFilter, expression);
                }

                var pageTarget = Service.GetPaged(paginate.Page, paginate.Size, expression);
                var result = new ResultODataInterno<T>() { __count = pageTarget.TotalItems, results = pageTarget.Data };
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Retorna uma entidade dado o seu ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade</returns>
        [HttpGet]
        [Route("LoadPaginate")]
        public virtual IActionResult LoadPaginate(int page, PageSize size)
        {
            try
            {
                //var expression = ;
                //if (paginate.PaginateFilter != null)
                //{
                //    expression = InterpreterFilterPaginate(paginate.PaginateFilter, expression);
                //}

                var pageTarget = Service.GetPaged(page, size, this.GetExpressionMandatoryPaginate());
                var result = new ResultODataInterno<T>() { __count = pageTarget.TotalItems, results = pageTarget.Data };
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }



        /// <summary>
        /// Retorna todas as entidades
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        [HttpGet]
        [Route("all")]
        public virtual IActionResult GetAll()
        {
            try
            {
                var result = Service.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [EnableQuery]
        public virtual IActionResult Get()
        {
            return Ok(Service.GetAll());
        }

        public class Paginate {
            public int Page { get; set; }
            public PageSize Size { get; set; }
            public PaginateFilter PaginateFilter { get; set; }
        }

        public class PaginateFilter : Filters
        {
            public string Clausule { get; set; }
            public List<Filters> Filters { get; set; }
        }
        public class Filters {
            public string Property { get; set; }
            public object Value { get; set; }
            public string Operator { get; set; }
        }
        
    }
}