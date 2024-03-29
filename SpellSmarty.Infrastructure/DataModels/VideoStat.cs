﻿using System;
using System.Collections.Generic;

namespace SpellSmarty.Infrastructure.DataModels
{
    public partial class VideoStat
    {
        public int StatId { get; set; }
        public int AccountId { get; set; }
        public int VideoId { get; set; }
        public string Progress { get; set; } = null!;

        public virtual Account Account { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
    }
}
