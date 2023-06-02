using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Dtos
{
    public class VideoDto
    {
        public double? Rating { get; set; }
        public string Subtitle { get; set; } = null!;
        public string SrcId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? ThumbnailLink { get; set; }
        public string? ChannelName { get; set; }
        public int LearntCount { get; set; }
        public string? VideoDescription { get; set; }
    }
}
