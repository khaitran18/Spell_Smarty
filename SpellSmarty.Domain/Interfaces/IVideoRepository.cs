using SpellSmarty.Domain.Models;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IVideoRepository
    {
        Task<IEnumerable<VideoModel>> GetAll();
    }
}
