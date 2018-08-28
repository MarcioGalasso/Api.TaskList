using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharedKernel.Domain.Entities;
using System;

namespace SharedKernel.Api.Extensions
{
    public static class HttpExtension
    {
        public static ApplicationUserViewModel GetApplicationUserFromHeaders(this HttpContext httpContext)
        {
            string apiClient = httpContext.Request.Headers["apiClient"].ToString();

            if (string.IsNullOrEmpty(apiClient))
                return new ApplicationUserViewModel();

            try
            {
                return JsonConvert.DeserializeObject<ApplicationUserViewModel>(apiClient);
            }
            catch(Exception ex)
            {
                return new ApplicationUserViewModel();
            }            
        }
    }
}
