using MediatR;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public class UpdateVideoGenreCommand : IRequest<BaseResponse<VideoGenreModel>>
    {
        public int VideoId { get; set; }
        public int GenreId { get; set; }
        public int VideoGenreId { get; set; }
    }
}
