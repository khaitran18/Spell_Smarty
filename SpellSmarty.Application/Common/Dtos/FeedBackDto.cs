using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Dtos
{
    public class FeedBackDto
    {
        public int FeedbackId { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime Date { get; set; }
    }
}
