using System;
using System.Collections.Generic;

namespace SpellSmarty.Infrastructure.DataModels
{
    public partial class Account
    {
        public Account()
        {
            Feedbacks = new HashSet<Feedback>();
            VideoStats = new HashSet<VideoStat>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Name { get; set; }
        public int Planid { get; set; }
        public DateTime? SubribeDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Plan Plan { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<VideoStat> VideoStats { get; set; }
    }
}
