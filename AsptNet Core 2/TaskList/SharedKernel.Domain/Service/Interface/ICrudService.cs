using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Modelo.Interface;

namespace SharedKernel.Domain.Services.Interface
{
    public interface ICrudService<T> : IQueryService<T> where T : EntityBase, IEntity
    {
        void Insert(string user, params T[] entities);
        void Insert(T entity, string user);
        void Update(string user, params T[] entities);
        void Update(T entity, string user);
        void Inactivate(T entity, string user);
        void Delete(T entity);
        void RunCommand(string hqlCommand);
    }
}