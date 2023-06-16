using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Domain.Interfaces
{
    public interface IFeedBackRepository : IBaseRepository<FeedBackModel>
    {
        Task<IEnumerable<FeedBackModel>> GetFeedBack();
    }
}
