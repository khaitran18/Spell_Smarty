using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateJWTToken((int userId, string userName, string roles) userDetails);
    }
}
