using MediatR;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{    public class UpdateVideoCommand : IRequest<VideoModel>
    {
        public int Videoid { get; set; }
        public string Subtitle { get; set; } = null!;
        public string? VideoDescription { get; set; }
        public int level { get; set; }
        public bool Premium { get; set; }
    }
}
