using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Response
{
    public abstract class BaseResponse
    {
        public bool Error { get; set; }
        public string? Message { get; set; }
        public Exception? Exception { get; set; }
    }
}
