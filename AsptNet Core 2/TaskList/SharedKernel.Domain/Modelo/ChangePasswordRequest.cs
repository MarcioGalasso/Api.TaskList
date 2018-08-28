namespace SharedKernel.Domain.Modelo
{
    public class ChangePasswordRequest
    {
        public string Login       { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Name { get; set; }
    }
}