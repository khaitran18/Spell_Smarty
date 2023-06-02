using System;
using System.Collections.Generic;

namespace SpellSmarty.Infrastructure.DataModels
{
    public partial class VideoGenre
    {
        public int VideoId { get; set; }
        public int GenreId { get; set; }
        public int VideoGenreId { get; set; }

        public virtual Genre Genre { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
    }
}
