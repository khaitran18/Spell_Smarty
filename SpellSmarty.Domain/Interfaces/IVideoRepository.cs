using SpellSmarty.Domain.Models;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IVideoRepository : IBaseRepository<VideoModel>
    {
        Task<IEnumerable<VideoModel>> GetAllWithGenre();
    }
}
