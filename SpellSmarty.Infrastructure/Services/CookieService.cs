using Microsoft.AspNetCore.Http;
using SpellSmarty.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Infrastructure.Services
{
    public class CookieService:ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool DeleteCookie(string key)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    httpContext.Response.Cookies.Delete(key);
                    return true;
                }
                else return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async void WriteCookie(string key, string value, int? expirationDays = null)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var options = new CookieOptions();

                if (expirationDays.HasValue)
                {
                    options.Expires = DateTime.UtcNow.AddDays(expirationDays.Value);
                    options.HttpOnly = true;
                    options.Secure = true;
                }
                httpContext.Response.Cookies.Append(key, value, options);
            }
        }
    }
}
