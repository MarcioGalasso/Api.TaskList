using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedKernel.Api.Extensions;
using SharedKernel.DependencyInjector;

using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Extensions;
using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.Services.Interface;
using SharedKernel.Domain.Validation;
using System;
using System.Linq;

namespace SharedKernel.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IApplicationUserService _appUserService;
        protected IConfiguration Configuration;

        public AccountController()
        {
            _usuarioService = Kernel.Get<IUsuarioService>();
            _appUserService = Kernel.Get<IApplicationUserService>();
            Configuration = Kernel.Get<IConfiguration>();
        }

        [HttpGet("teste")]
        public IActionResult teste() {
            return Ok(true);
        }

        [HttpPost("changepassword")]
        public IActionResult ChangePassword([FromBody]PasswordViewModel changePassword)
        {
            try
            {
                if (changePassword == null)
                    return Ok(false);

                ApplicationUserViewModel appUserVm = HttpContext.GetApplicationUserFromHeaders();

                if (appUserVm == null)
                    return Ok(false);

                ApplicationUser appUser = _appUserService.GetAll(x => x.Name == appUserVm.Name && x.Secret == appUserVm.Secret).FirstOrDefault();

                if (appUser == null)
                    return Ok(false);

                Usuario usuario = this._usuarioService.GetAll(x => x.Key == changePassword.Key && x.Email == changePassword.Email).FirstOrDefault();

                if (usuario == null)
                    return Ok(false);

                if (changePassword.NewPassword != changePassword.ConfirmNewPassword)
                    return Ok(false);

                ChangePasswordRequest newPassword = new ChangePasswordRequest()
                {
                    Login = usuario.Login,
                    NewPassword = changePassword.NewPassword,
                    OldPassword = usuario.Senha
                };
                this._usuarioService.TrocaSenha(newPassword, false);

                return Ok(true);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }            
        }

        [HttpPost("recover")]
        public IActionResult Recover([FromBody]ActivateViewModel activate)
        {
            try
            {
                if (string.IsNullOrEmpty(activate?.Key))
                    return Ok(false);

                ApplicationUserViewModel appUserVm = HttpContext.GetApplicationUserFromHeaders();

                if (appUserVm == null)
                    return Ok(false);

             

                Usuario usuario = this._usuarioService.GetAll(x => x.Key == activate.Key).FirstOrDefault();

                if (usuario == null)
                    return Ok(false);

                var user = new
                {
                    Email = usuario.Email,
                    Login = usuario.Login
                };

                return Ok(user);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("recover/{email}")]
        public IActionResult Recover(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return Ok(false);

                ApplicationUserViewModel appUserVm = HttpContext.GetApplicationUserFromHeaders();

                if (appUserVm == null)
                    return Ok(false);

              

                Usuario usuario = this._usuarioService.GetAll(x => x.Email == email).FirstOrDefault();

                if (usuario == null)
                    return Ok(false);

                usuario.UpdateKey();
                this._usuarioService.Update(usuario, usuario.Nome);
                
                
                return Ok(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }            
        }

        [HttpPost("activate")]
        public IActionResult PostActivate([FromBody]ActivateViewModel activate)
        {
            try
            {
                if (String.IsNullOrEmpty(activate?.Key))
                    return Ok(false);

                ApplicationUserViewModel appUserVm = HttpContext.GetApplicationUserFromHeaders();

                if (appUserVm == null)
                    return Ok(false);

               

                Usuario usuario = this._usuarioService.GetAll((us) => us.Key == activate.Key).FirstOrDefault();

                if (usuario == null)
                    return Ok(false);

                usuario.EmailIsConfirmed = true;
                this._usuarioService.Update(usuario, usuario.Email);

                return Ok(true);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserViewModel user)
        {
            try
            {
                if (user == null)
                    return Ok(false);

                ApplicationUserViewModel appUserVm = HttpContext.GetApplicationUserFromHeaders();

                if (appUserVm == null)
                    return Ok(false);

              
                Usuario usuario = this._usuarioService.GetAll(x => x.Email == user.Email).FirstOrDefault();

                if(usuario != null)
                    return Ok(false);

                usuario = new Usuario()
                {
                    Email = user.Email,
                    Bloqueado = false,
                    ForcarTrocaDeSenha = false,
                    Login = user.Email,
                    EmailIsConfirmed = true,
                    Nome = user.Name,
                    QtdeConexoesSimultaneasPermitidas = 3,
                    QtdeLoginsErrados = 0,
                    QtdeLoginsErradosParaBloquear = 9999,
                    Senha = user.Password
                };
                usuario.UpdateKey();

                this._usuarioService.Insert(usuario, usuario.Nome);

              
              
                return Ok(true);
            }
            catch (ValidatorException ex)
            {
                return Ok(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
