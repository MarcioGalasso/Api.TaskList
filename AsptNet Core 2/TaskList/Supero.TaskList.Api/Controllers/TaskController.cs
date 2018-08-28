using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Api.Controllers;
using SharedKernel.Domain.Services.Interface;
using Supero.TaskList.Domain.Modelo;
using Supero.TaskList.Domain.Services;

namespace Supero.TaskList.Api.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : BaseController<Task>
    {
        public TaskController(TaskService service) : base(service)
        {
        }
        
    }
}
