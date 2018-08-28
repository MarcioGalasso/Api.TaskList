using System.Linq;
using Microsoft.EntityFrameworkCore;
using SharedKernel.EntityFramework.Maps;
using Supero.TaskList.Maps.Maps;

namespace Supero.TaskList.Maps
{
    // TODO: Algorama - Decidir o que fazer sobre o problema do Lazy Loading
    public class TaskListContext : DbContext
    {
        public TaskListContext(DbContextOptions<TaskListContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TaskMap());
            modelBuilder.ApplyConfiguration(new ApplicationUserMap());


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}