using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Models
{
    public class FeedBackModel
    {
        public int FeedbackId { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime Date { get; set; }
    }
}
