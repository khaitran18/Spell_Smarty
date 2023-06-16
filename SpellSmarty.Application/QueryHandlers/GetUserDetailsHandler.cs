using AutoMapper;
using MediatR;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.QueryHandlers
{
    public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsQuery, AccountModel?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenService;

        public GetUserDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenServices tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<AccountModel?> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var claims = _tokenService.ValidateToken(request.token);
            int accountId = int.Parse(claims.FindFirst("jti")?.Value ?? "0");
            AccountModel? account = await _unitOfWork.AccountRepository.GetUserDetailsAsync(accountId);
            return account;
        }
    }
}
