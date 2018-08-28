using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using SharedKernel.Domain.Extensions;
using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Validation;
using SharedKernel.Domain.Services.Interface;
using SharedKernel.Domain.Enum;
using SharedKernel.Domain.Helpers;

namespace SharedKernel.Domain.Services
{
    public class UsuarioService : CrudService<Usuario>, IUsuarioService
    {
        public UsuarioService(IRepository<Usuario> repository, UsuarioValidator validator, IConfiguration configuration,
            IRestService restService) : base(repository, validator, configuration, restService)
        {
            validator.Service = this;
        }

        public override Usuario Get(long id, bool loadFirstChild = false)
        {
            var usuario = base.Get(id, loadFirstChild);
            usuario.Senha = null;
            return usuario;
        }

        public override PageResult<Usuario> GetPaged(int page, PageSize size, Expression<Func<Usuario,bool>> where)
        {
            var usuarios = base.GetPaged(page, size, where);
            foreach (var usuario in usuarios.Data)
            {
                usuario.Senha = null;
            }
            return usuarios;
        }

        public override void Insert(Usuario entidade, string user = "sistema")
        {
            entidade.Senha = CryptoTools.ComputeHashMd5(entidade.Senha);
            DefineRegrasParaTrocaSenha(entidade);
            base.Insert(entidade, user);
        }

        public override void Insert(string user, params Usuario[] entities)
        {
            foreach (var entidade in entities)
            {
                entidade.Senha = CryptoTools.ComputeHashMd5(entidade.Senha);
                DefineRegrasParaTrocaSenha(entidade);
            }
            base.Insert(user, entities);
        }

        public override void Update(Usuario entidade, string user = "sistema")
        {
            var usuarioOld = base.Get(entidade.Id);
            entidade.Senha = usuarioOld.Senha;
            entidade.Foto = usuarioOld.Foto;
            DefineRegrasParaTrocaSenha(entidade);

            entidade.AddInverseReferences(user);

            var result = Validator.Validate(entidade, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                entidade.createDateTime = usuarioOld.createDateTime;
                entidade.createUserName = usuarioOld.createUserName;
                entidade.updateDateTime = DateTime.Now;
                entidade.updateUserName = user;
                Repository.Update(entidade);
                Repository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePerfil(Usuario entidade, string user = "sistema")
        {
            var usuarioOld = base.Get(entidade.Id);
            entidade.Senha = usuarioOld.Senha;
            base.Update(entidade, user);
        }

        private static void DefineRegrasParaTrocaSenha(Usuario entidade)
        {
            if (entidade.ForcarTrocaDeSenha)
            {
                if (entidade.DataDaUltimaTrocaDeSenha == null)
                    entidade.DataDaUltimaTrocaDeSenha = DateTime.Today;

                entidade.DataDaProximaTrocaDeSenha = entidade.IntervaloDiasParaTrocaDeSenha > 0
                    ? entidade.DataDaUltimaTrocaDeSenha.Value.AddDays(entidade.IntervaloDiasParaTrocaDeSenha)
                    : DateTime.Today;
            }
            else
            {
                entidade.DataDaProximaTrocaDeSenha = null;
            }
        }

        public virtual Usuario GetUser(LoginRequest loginRequest) {
            return  GetAll(x =>
              x.Login.ToUpper() == loginRequest.Login.ToUpper() && x.Bloqueado == false && x.EmailIsConfirmed).FirstOrDefault();
        }

        public Token Login(LoginRequest loginRequest)
        {
            var senha = CryptoTools.ComputeHashMd5(loginRequest.Password);
            var usuario = GetUser(loginRequest);
            if (usuario == null) return null;

            if (usuario.Senha == senha)
            {
                usuario.QtdeLoginsErrados = 0;
                Update(usuario);

                var token = new Token
                {
                    UsuarioId = usuario.Id,
                    UsuarioNome = usuario.Nome,
                    Login = usuario.Login,
                    DataExpiracao = DateTime.Now.AddHours(12),
                    Email = usuario.Email
                };
                return token;
            }

            usuario.QtdeLoginsErrados++;

            if (usuario.QtdeLoginsErrados >= usuario.QtdeLoginsErradosParaBloquear)
                usuario.Bloqueado = true;

            Update(usuario);

            return null;
        }

        public void TrocaSenha(ChangePasswordRequest changePasswordRequest, bool encryptOldPassword = true)
        {
            var senhaAntiga = (encryptOldPassword) ? CryptoTools.ComputeHashMd5(changePasswordRequest.OldPassword) : changePasswordRequest.OldPassword;
            var usuario = GetAll(x =>
                x.Login.ToUpper() == changePasswordRequest.Login.ToUpper() &&
                x.Senha == senhaAntiga).FirstOrDefault();

            if (usuario == null)
                throw new ValidationException("Senha antiga não confere");

            usuario.Senha = CryptoTools.ComputeHashMd5(changePasswordRequest.NewPassword);
            usuario.DataDaUltimaTrocaDeSenha = DateTime.Today;

            if (!string.IsNullOrEmpty(changePasswordRequest.Name))
                usuario.Nome = changePasswordRequest.Name;
            
            if (usuario.ForcarTrocaDeSenha)
            {
                if (usuario.IntervaloDiasParaTrocaDeSenha > 0)
                    usuario.DataDaProximaTrocaDeSenha = usuario.DataDaUltimaTrocaDeSenha.Value.AddDays(usuario.IntervaloDiasParaTrocaDeSenha);
                else
                {
                    usuario.ForcarTrocaDeSenha = false;
                    usuario.DataDaProximaTrocaDeSenha = null;
                }
            }

            this.Update(usuario, usuario.Email);         
        }

        public string GetTema(long usuarioId)
        {
            var usuario = Get(usuarioId);
            if(usuario == null)
                throw new ValidationException("Usuário Inválido!");

            return usuario.Tema;
        }

        public void ChangeTema(long usuarioId, string newTema)
        {
            if(string.IsNullOrEmpty(newTema))
                throw new ValidationException("Tema Inválido!");

            var usuario = Get(usuarioId);
            if (usuario == null)
                throw new ValidationException("Usuário Inválido!");

            usuario.Tema = newTema;

            Update(usuario, "ChangeTema");
        }
    }
}