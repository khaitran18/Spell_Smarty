using SpellSmarty.Application.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Dtos
{
    public class AuthResponseDto : BaseResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
