using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public int Planid { get; set; }
        public DateTime? SubribeDate { get; set; }
        public DateTime? EndDate { get; set; }

        //public virtual PlanDto Plan { get; set; } = null!;
    }

    public class PlanDto
    {
        public int Planid { get; set; }
        public string PlanName { get; set; } = null!;
    }
}
