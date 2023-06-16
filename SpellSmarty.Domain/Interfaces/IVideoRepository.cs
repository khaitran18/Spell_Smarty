using SpellSmarty.Domain.Models;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IVideoRepository : IBaseRepository<VideoModel>
    {
        Task<IEnumerable<VideoModel>> GetAllWithGenre();
        Task<VideoModel> GetVideoById(int videoid);
        Task<IEnumerable<VideoModel>> GetVideosByUserId(int userId);
        Task<IEnumerable<VideoModel>> GetVideosByCreator(int videoId);
        Task<IEnumerable<VideoModel>> GetVideoByGenre(int videoId);
        Task<VideoModel> SaveVideo(float? rating,
                                string subtitle,
                                string? thumbnaillink,
                                string? channelname,
                                string srcid,
                                string title,
                                int learntcount,
                                string description,
                                int level,
                                bool premium);
        Task<VideoModel> UpdateVideo(int videoid, string subtitle, string srcid, string title, string description, int level, bool premium);
    }
}
