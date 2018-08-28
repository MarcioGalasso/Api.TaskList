using SharedKernel.Domain.Helpers;
using System;

namespace SharedKernel.Domain.Modelo
{
    public class Usuario : EntityBase
    {               
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Tema { get; set; }
        public string Email { get; set; }
        public byte[] Foto { get; set; }
        public bool Bloqueado { get; set; }
        public int QtdeLoginsErradosParaBloquear { get; set; }
        public int QtdeLoginsErrados { get; set; }
        public int QtdeConexoesSimultaneasPermitidas { get; set; }
        public bool ForcarTrocaDeSenha { get; set; }
        public int IntervaloDiasParaTrocaDeSenha { get; set; }
        public DateTime? DataDaUltimaTrocaDeSenha { get; set; }
        public DateTime? DataDaProximaTrocaDeSenha { get; set; }
        public bool EmailIsConfirmed { get; set; }
        public string Key { get; set; }
        public DateTime? KeyDate { get; set; }

        public Usuario()
        {
            Bloqueado = false;
            QtdeLoginsErradosParaBloquear = 3;
            QtdeLoginsErrados = 0;
            QtdeConexoesSimultaneasPermitidas = 1;
            ForcarTrocaDeSenha = false;
            IntervaloDiasParaTrocaDeSenha = 0;
        }

        public void UpdateKey()
        {
            KeyDate = DateTime.Now;
            Key = CryptoTools.ComputeHashSha256(Nome + Email + Guid.NewGuid().ToString()).Replace("-", "");
        }
    }
}