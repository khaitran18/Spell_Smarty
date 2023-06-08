using SpellSmarty.Domain.Models;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IVideoRepository : IBaseRepository<VideoModel>
    {
        Task<IEnumerable<VideoModel>> GetAllWithGenre();
        Task<VideoModel> GetVideoById(int videoid);
        Task<IEnumerable<VideoModel>> GetVideosByUserId(int userId);
        Task<IEnumerable<VideoModel>> GetVideosByCreator(string creator);
    }
}
