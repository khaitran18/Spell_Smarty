using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Dtos
{
    public class VideoStatDto
    {
        public int StatId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public int Progress { get; set; }
    }
}
