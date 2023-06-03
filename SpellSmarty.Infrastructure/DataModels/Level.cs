using System;
using System.Collections.Generic;

namespace SpellSmarty.Infrastructure.DataModels
{
    public partial class Level
    {
        public Level()
        {
            Videos = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Video> Videos { get; set; }
    }
}
