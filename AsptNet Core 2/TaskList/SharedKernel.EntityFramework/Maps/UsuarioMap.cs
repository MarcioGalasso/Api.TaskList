using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Modelo;

namespace SharedKernel.EntityFramework.Maps
{
    public class UsuarioMap : BaseMap, IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> map)
        {
            map.Property(x => x.Nome);
            map.Property(x => x.Login);
            map.HasIndex(x => x.Login);
            map.Property(x => x.Senha);
            map.Property(x => x.Tema);
            map.Property(x => x.Email);
            map.Property(x => x.Foto);
            map.Property(x => x.Bloqueado);
            map.Property(x => x.QtdeLoginsErradosParaBloquear);
            map.Property(x => x.QtdeLoginsErrados);
            map.Property(x => x.QtdeConexoesSimultaneasPermitidas);
            map.Property(x => x.ForcarTrocaDeSenha);
            map.Property(x => x.IntervaloDiasParaTrocaDeSenha);
            map.Property(x => x.DataDaUltimaTrocaDeSenha);
            map.Property(x => x.DataDaProximaTrocaDeSenha);
            map.Property(x => x.EmailIsConfirmed).HasColumnName("EmailIsConfirmed");
            map.Property(x => x.Key).HasMaxLength(150).HasColumnName("Key");
            map.Property(x => x.KeyDate).HasColumnName("KeyDate");

            ConfigureMap(map, "pk_usuario");
        }
    }
}