using System;

namespace SharedKernel.Domain.Modelo
{
    public class Token
    {
        public long     UsuarioId       { get; set; }
        public string   UsuarioNome     { get; set; }
        public string   Login           { get; set; }
        public DateTime DataExpiracao   { get; set; }
        public bool     DeveTrocarSenha { get; set; }
        public bool     Admin { get; set; } = false;
        public string   Email { get; set; }
    }
}