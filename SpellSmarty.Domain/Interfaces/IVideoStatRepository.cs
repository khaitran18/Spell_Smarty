using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IVideoStatRepository : IBaseRepository<VideoStatModel>
    {
        Task<VideoStatModel> SaveProgress(int userId, int statid, int progress);
        Task<string> GetProgressByUserIdAndVideoId(int userId, int videoId);
    }
}
