using SharedKernel.Domain.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Supero.TaskList.Domain.Modelo
{
   public class Task : EntityBase
    {
        public string Descricao { get; set; }
        public int Status { get; set; }

    }
}
