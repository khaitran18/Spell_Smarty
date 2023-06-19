using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Models
{
    public class VideoGenreModel
    {
        public int VideoId { get; set; }
        public int GenreId { get; set; }
        public int VideoGenreId { get; set; }
    }
}
