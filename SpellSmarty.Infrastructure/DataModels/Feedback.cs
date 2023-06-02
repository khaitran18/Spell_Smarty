using System;
using System.Collections.Generic;

namespace SpellSmarty.Infrastructure.DataModels
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime Date { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
