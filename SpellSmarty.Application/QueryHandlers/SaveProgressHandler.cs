using AutoMapper;
using MediatR;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Queries;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Domain.Models;


namespace SpellSmarty.Application.QueryHandlers
{
    public class SaveProgressHandler : IRequestHandler<SaveProgressQuery, VideoStatDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveProgressHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VideoStatDto> Handle(SaveProgressQuery request, CancellationToken cancellationToken)
        {
            VideoStatDto listDto = _mapper.Map<VideoStatDto>(await _unitOfWork.VideoStatRepository.SaveProgress(request.statId ,request.progress));
            return listDto;
        }
    }
}
