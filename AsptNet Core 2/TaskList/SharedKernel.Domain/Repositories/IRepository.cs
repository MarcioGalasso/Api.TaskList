
using SharedKernel.Domain.Modelo.Interface;
using System.Collections.Generic;

namespace SharedKernel.Domain.Repositories
{
    public interface IRepository<T> : IQueryRepository<T> where T : IEntity
    {
        void Insert(T entity);
        void Insert(IList<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        int RunCommand(string command, params object[] poParams); 
    }
}