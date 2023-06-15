using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Services
{
    public interface ICookieService
    {
        public void WriteCookie(string key, string value, int? expirationDays = null);
        public bool DeleteCookie(string key);
    }
}
