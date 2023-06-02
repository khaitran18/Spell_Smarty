using System;
using System.Collections.Generic;

namespace SpellSmarty.Infrastructure.DataModels
{
    public partial class Plan
    {
        public Plan()
        {
            Accounts = new HashSet<Account>();
        }

        public int Planid { get; set; }
        public string PlanName { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
