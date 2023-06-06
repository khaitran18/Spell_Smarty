using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVideoRepository VideosRepository { get; }
        IVideoStatRepository VideoStatRepository { get; }
        void Save();
    }
}
