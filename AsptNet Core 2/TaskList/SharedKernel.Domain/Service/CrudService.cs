using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SharedKernel.Domain.Extensions;
using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Modelo.Interface;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Services.Interface;
using SharedKernel.Domain.Validation;

namespace SharedKernel.Domain.Services
{
    public class CrudService<T> : QueryService<T>, ICrudService<T> where T : EntityBase, IEntity
    {
        public new IRepository<T> Repository { get; }

        protected Validator<T> Validator { get; set; }

        public CrudService(IRepository<T> repository, Validator<T> validator, IConfiguration configuration, 
            IRestService restService) : base(repository, configuration, restService)
        {
            Repository = repository;
            Validator = validator;
        }
        
        public virtual void Insert(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Insert);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                entity.createDateTime = DateTime.Now;
                entity.createUserName = user;
                Repository.Insert(entity);
                Repository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Insert(string user, params T[] entities)
        {
            var result = Validator.Validate(entities, ValidationTypes.Insert);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {                
                foreach (var entity in entities)
                {
                    entity.AddInverseReferences(user);

                    entity.createDateTime = DateTime.Now;
                    entity.createUserName = user;
                }

                Repository.Insert(entities);
                Repository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Update(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            T entityOld = Get(entity.Id);

            try
            {
                entity.createDateTime = entityOld.createDateTime;
                entity.createUserName = entityOld.createUserName;
                entity.updateDateTime = DateTime.Now;
                entity.updateUserName = user;
                Repository.Update(entity);
                Repository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Inactivate(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                entity.deleteDateTime = DateTime.Now;
                entity.updateUserName = user;
                Repository.Update(entity);
                Repository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Update(string user, params T[] entities)
        {
            var result = Validator.Validate(entities, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                foreach (var entity in entities)
                {
                    entity.AddInverseReferences(user);
                    entity.updateDateTime = DateTime.Now;
                    entity.updateUserName = user;
                    Repository.Update(entity);
                }
                Repository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Delete(T entity)
        {
            var result = Validator.Validate(entity, ValidationTypes.Delete);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                Repository.Delete(entity);
                Repository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        public virtual void RunCommand(string command)
        {
            try
            {
                Repository.RunCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}