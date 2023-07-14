using AutoMapper;
using MediatR;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.QueryHandlers
{
    public class GetFeedBackHandler : IRequestHandler<GetFeedBackQuery, BaseResponse<IEnumerable<FeedBackDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeedBackHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<FeedBackDto>>> Handle(GetFeedBackQuery request, CancellationToken cancellationToken)
        {
            BaseResponse<IEnumerable<FeedBackDto>> response = new BaseResponse<IEnumerable<FeedBackDto>>();
            try
            {
                List<FeedBackDto> listDto = _mapper.Map<List<FeedBackDto>>(await _unitOfWork.FeedBackRepository.GetFeedBack());
                response.Result = listDto;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Exception = ex;
                response.Message = "Error in the server";
            }
            return response;
           
        }
    }
}
