using SharedKernel.Domain.Modelo.Interface;
using System;

namespace SharedKernel.Domain.Modelo
{
    public class EntityBase : Entity<int> 
    {
        public DateTime? createDateTime  { get; set; }
        public string createUserName  { get; set; }
        public DateTime? updateDateTime  { get; set; }
        public string updateUserName  { get; set; }
        public DateTime? deleteDateTime  { get; set; }
        public string deleteUserName { get; set; }

        public bool isDeleted => deleteDateTime != null;

        public EntityBase Clone()
        {
            return (EntityBase)MemberwiseClone();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}