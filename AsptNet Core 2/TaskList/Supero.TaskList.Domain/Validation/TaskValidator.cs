using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.Validation;
using Supero.TaskList.Domain.Modelo;
using Supero.TaskList.Domain.Services;

namespace Supero.TaskList.Domain.Validation
{
    public class TaskValidator : Validator<Task>
    {
        public TaskService Service { get; set; }
    }
}