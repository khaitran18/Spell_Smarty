using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Models
{
    public class VideoStatModel
    {
        public int StatId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string? Progress { get; set; }
    }
}
