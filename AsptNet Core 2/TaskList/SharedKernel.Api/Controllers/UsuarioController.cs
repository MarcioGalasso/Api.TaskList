using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Api.Controllers
{
    public class UsuarioController : BaseController<Usuario>
    {
        public UsuarioController(ICrudService<Usuario> service) : base(service)
        {

        }

    }
}
