using System;
using System.Collections.Generic;

namespace SpellSmarty.Infrastructure.DataModels
{
    public partial class Video
    {
        public Video()
        {
            VideoGenres = new HashSet<VideoGenre>();
            VideoStats = new HashSet<VideoStat>();
        }

        public int Videoid { get; set; }
        public double? Rating { get; set; }
        public string Subtitle { get; set; } = null!;
        public string SrcId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? ThumbnailLink { get; set; }
        public string? ChannelName { get; set; }
        public int LearntCount { get; set; }
        public string? VideoDescription { get; set; }
        public int Level { get; set; }
        public DateTime AddedDate { get; set; }
        public virtual Level LevelNavigation { get; set; } = null!;
        public virtual ICollection<VideoGenre> VideoGenres { get; set; }
        public virtual ICollection<VideoStat> VideoStats { get; set; }
    }
}
