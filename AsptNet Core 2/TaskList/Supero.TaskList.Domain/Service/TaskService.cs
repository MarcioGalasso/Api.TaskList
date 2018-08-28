using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using SharedKernel.Domain.Extensions;
using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Validation;
using SharedKernel.Domain.Services.Interface;
using SharedKernel.Domain.Enum;
using SharedKernel.Domain.Helpers;
using Supero.TaskList.Domain.Modelo;
using Supero.TaskList.Domain.Validation;
using SharedKernel.Domain.Services;
using static Supero.TaskList.Domain.Enum.EnumTask;

namespace Supero.TaskList.Domain.Services
{
    public class TaskService : CrudService<Task>
    {
        public TaskService(IRepository<Task> repository, TaskValidator validator, IConfiguration configuration,
            IRestService restService) : base(repository, validator, configuration, restService)
        {
            validator.Service = this;
        }

        public override void Update(Task entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            Task entityOld = Get(entity.Id);

            try
            {
                entity.createDateTime = entityOld.createDateTime;
                entity.createUserName = entityOld.createUserName;
                if (entity.Status == (int)StatusTask.concluido) {
                    entity.updateDateTime = DateTime.Now;
                }
                else {
                    entity.updateDateTime = null;
                }
                entity.updateUserName = user;
                Repository.Update(entity);
                Repository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}