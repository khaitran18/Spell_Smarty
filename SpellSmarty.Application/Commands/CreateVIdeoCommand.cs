using MediatR;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public class CreateVideoCommand : IRequest<VideoModel>
    {
        public float? rating { get; set; }
        public string subtitle { get; set; }
        public string? thumbnaillink { get; set; }
        public string? channelname { get; set; }
        public string srcid { get; set; }
        public string title { get; set; }
        public int learntcount { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public bool premium { get; set; }
    }
}
