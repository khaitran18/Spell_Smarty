using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public int Planid { get; set; } = 1;
        public DateTime? SubribeDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool EmailVerify { get; set; }
    }
}
