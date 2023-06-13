using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Dtos
{
    public class VideoDto
    {
        public int Videoid { get; set; }
        public double? Rating { get; set; }
        public string? Subtitle { get; set; } = null!;
        public string SrcId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? ThumbnailLink { get; set; }
        public string? ChannelName { get; set; }
        public int LearntCount { get; set; }
        public string? VideoDescription { get; set; }
        public string level { get; set; }
        public DateTime AddedDate { get; set; }
        public IEnumerable<string> VideoGenres { get; set; }
        public string? progress { get; set; }
        public bool Premium { get; set; }
    }
}
