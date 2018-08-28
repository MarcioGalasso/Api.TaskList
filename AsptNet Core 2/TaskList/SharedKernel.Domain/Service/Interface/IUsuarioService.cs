using SharedKernel.Domain.Modelo;


namespace SharedKernel.Domain.Services.Interface
{
    public interface IUsuarioService : ICrudService<Usuario>
    {
        Token Login(LoginRequest loginRequest);
        void TrocaSenha(ChangePasswordRequest changePasswordRequest, bool encryptOldPassword = true);
        string GetTema(long usuarioId);
        void ChangeTema(long usuarioId, string newTema);
        void UpdatePerfil(Usuario entity, string user);
    }
}