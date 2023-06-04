using Microsoft.EntityFrameworkCore;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly SpellSmartyContext _context;

        public BaseRepository(SpellSmartyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
