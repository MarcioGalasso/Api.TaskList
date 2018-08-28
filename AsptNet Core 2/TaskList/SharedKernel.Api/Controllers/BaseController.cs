using Microsoft.AspNetCore.Mvc;
using SharedKernel.Api.AuthorizationFilter;
using SharedKernel.Domain.Services.Interface;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel.Domain.Modelo.Interface;
using SharedKernel.Domain.Validation;
using SharedKernel.Api.Security;

namespace SharedKernel.Api.Controllers
{
    [UserAuthorization]
    public class BaseController<T> : QueryController<T> where T : EntityBase, IEntity
    {
        protected new ICrudService<T> Service { get; set; }

        public BaseController(ICrudService<T> service) : base(service)
        {
            Service = service;
        }

        /// <summary>
        /// Incluí uma nova entidade (INSERT)
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <returns>Entidade inclusa</returns>
        [HttpPost]
        public virtual IActionResult Post([FromBody]T entity)
        {
            try
            {
                var token = HttpContext.RecuperarToken();

                Service.Insert(entity, token?.Login ?? "");

                // TODO: Recuperar UrlHelper
                // var helper = new UrlHelper(Request);
                // var location = helper.Link("DefaultApi", new { id = entity.Id });

                // return Created(location, entity);

                return Ok(entity);
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Atualiza dados da entidade (UPDATE)
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <returns>Entidade atualizada</returns>
        [HttpPut]
        public virtual IActionResult Put([FromBody]T entity)
        {
            try
            {
                var token = HttpContext.RecuperarToken();
                Service.Update(entity, token?.Login);

                return Ok(entity);
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Inativa uma Entidade, atribuindo a data do dia para o deletedDateTime
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <returns>Entidade atualizada</returns>
        [HttpPut]
        [Route("inactivate")]
        public virtual IActionResult PutInactivate([FromBody]T entity)
        {
            try
            {
                var token = HttpContext.RecuperarToken();
                Service.Inactivate(entity, token?.Login);

                return Ok(entity);
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Excluí a entidade (DELETE)
        /// </summary>
        /// <param name="id">ID da Entidade</param>
        /// <returns>OK</returns>
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(long id)
        {
            try
            {
                var entity = Service.Get(id);
                if (entity == null)
                    return NotFound();

                Service.Delete(entity);

                return Ok();
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
