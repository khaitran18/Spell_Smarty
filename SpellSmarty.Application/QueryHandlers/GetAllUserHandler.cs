using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.QueryHandlers
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, IEnumerable<AccountModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountModel>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            List<AccountModel> listDto = _mapper.Map<List<AccountModel>>(await _unitOfWork.AccountRepository.GetAllAccountsAsync());
            return listDto;
        }
    }
}
