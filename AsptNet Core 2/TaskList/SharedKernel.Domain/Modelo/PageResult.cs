using System.Collections.Generic;
using SharedKernel.Domain.Modelo.Interface;

namespace SharedKernel.Domain.Modelo
{
    public class PageResult<T> where T : IEntity
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public string PreviousPage { get; set; }
        public string NextPage { get; set; }

        public IList<T> Data { get; set; }
    }
}