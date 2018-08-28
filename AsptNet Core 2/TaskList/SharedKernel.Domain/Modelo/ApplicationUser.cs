using SharedKernel.Domain.Modelo;

namespace SharedKernel.Domain.Entities
{
    public class ApplicationUser : EntityBase
    {
        public string Name { get; set; }
        public string Secret { get; set; }
    }
}
