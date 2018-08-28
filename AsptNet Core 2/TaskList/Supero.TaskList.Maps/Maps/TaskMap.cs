using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.EntityFramework.Maps;
using Supero.TaskList.Domain.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Supero.TaskList.Maps.Maps
{
    public class TaskMap : BaseMap, IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> map)
        {
            map.Property(x => x.Descricao);
            map.Property(x => x.Status);

            ConfigureMap(map, "pk_Task");
        }
    }
}
   