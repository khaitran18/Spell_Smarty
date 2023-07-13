using MediatR;
using SpellSmarty.Application.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Commands
{
    public class LogoutCommand : IRequest<BaseResponse<bool>>
    {

    }
}
