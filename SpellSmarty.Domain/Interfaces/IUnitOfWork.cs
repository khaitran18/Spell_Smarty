
namespace SpellSmarty.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVideoRepository VideosRepository { get; }
        IAccountRepository AccountRepository { get; }
        IVideoStatRepository VideoStatRepository { get; }
        void Save();
    }
}
