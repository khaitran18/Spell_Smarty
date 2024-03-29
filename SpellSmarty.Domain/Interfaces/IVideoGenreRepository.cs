﻿using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IVideoGenreRepository : IBaseRepository<VideoGenreModel>
    {
        Task<VideoGenreModel> AddVideoGenre(int videoId, int genreId);
        Task<VideoGenreModel> UpdateVideoGenre(int videoId);
    }
}
