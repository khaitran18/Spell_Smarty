using System;
using System.Collections.Generic;

namespace SpellSmarty.Infrastructure.DataModels
{
    public partial class Genre
    {
        public Genre()
        {
            VideoGenres = new HashSet<VideoGenre>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;

        public virtual ICollection<VideoGenre> VideoGenres { get; set; }
    }
}
