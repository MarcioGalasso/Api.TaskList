namespace SharedKernel.Domain.Entities
{
    public class PasswordViewModel
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Key { get; set; }
    }
}
