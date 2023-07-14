using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Common.Response;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.QueryHandlers;
using SpellSmarty.Infrastructure.DataModels;

namespace SpellSmarty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<VideoController>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize(Roles = "Free,Premium")]
        [HttpGet]
        public async Task<IActionResult> GetVideos()
        {
            var response = _mediator.Send(new GetVideosQuery()).Result;
            if (!response.Error) 
                return Ok(response.Result);
            else
            {
                var ErrorResponse = new BaseResponse<Exception>
                {
                    Exception = response.Exception,
                    Message = response.Message
                };
                return new ErrorHandling<Exception>(ErrorResponse);
            }
        }

        // GET api/<VideoController>/5
        [Route("GetVideoByVideoId")]
        [HttpGet()]
        public async Task<ActionResult> GetSingleVideo([FromHeader] string? Authorization, int videoId)
        {
            var video = await _mediator.Send(new GetSingleVideoQuery(videoId, Authorization));
            return Ok(video);
        }

        [Route("GetVideosByCreator")]
        [HttpGet()]
        public async Task<ActionResult> GetVideosByCreator(int videoId)
        {
            var video = await _mediator.Send(new GetVideosByCreatorQuery(videoId));
            return Ok(video);
        }
        [Route("GetVideoByGenre")]
        [HttpGet()]
        public async Task<ActionResult> GetVideosByGenre(int videoId)
        {
            var videos = await _mediator.Send(new GetVideosByGenreQuery(videoId));
            return Ok(videos);
        }
        [Route("GetVideoByUserId")]
        [HttpGet()]
        public async Task<ActionResult> GetVideosByUserId([FromHeader] string? Authorization)
        {
            var videos = await _mediator.Send(new GetVideosByUserIdQuery(Authorization));
            return Ok(videos);
        }

        [Route("Feedback")]
        [HttpGet()]
        public async Task<IActionResult> GetFeedBack()
        {
            var videos = await _mediator.Send(new GetFeedBackQuery());
            return Ok(videos);
        }
        [HttpPost("feedback/{videoid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Free,Premium")]
        [ProducesDefaultResponseType(typeof(FeedBackDto))]
        public async Task<IActionResult> CreateFeedBack(
            [FromHeader] string? Authorization
            , [FromBody] CreateFeedbackCommand command
            , [FromRoute] int videoid)
        {
            command.videoId = videoid;
            command.token = Authorization;
            var response = await _mediator.Send(command);
            if (!response.Error) return Ok(response.Result);
            else
            {
                var ErrorResponse = new BaseResponse<Exception>
                {
                    Exception = response.Exception,
                    Message = response.Message
                };
                return new ErrorHandling<Exception>(ErrorResponse);
            }
        }

        // POST api/<VideoController>
        [Route("SaveProgress")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Free,Premium")]
        public async Task<IActionResult> SaveProgress([FromHeader] string? Authorization, int videoId, string progress)
        {
            var response = await _mediator.Send(new SaveProgressQuery(Authorization, videoId, progress));
            if (!response.Error) return Ok(response.Result);
            else
            {
                var ErrorResponse = new BaseResponse<Exception>
                {
                    Exception = response.Exception,
                    Message = response.Message
                };
                return new ErrorHandling<Exception>(ErrorResponse);
            }
        }

        [Route("CreateGenre")]
        [HttpPost]
        public async Task<IActionResult> CreateGenre(string genreName)
        {
            var response = await _mediator.Send(new AddGenreCommand(genreName));
            if (!response.Error) return Ok(response.Result);
            else
            {
                var ErrorResponse = new BaseResponse<Exception>
                {
                    Exception = response.Exception,
                    Message = response.Message
                };
                return new ErrorHandling<Exception>(ErrorResponse);
            }
        }

        [Route("CreateNewVideo")]
        [HttpPost]
        public async Task<IActionResult> CreateVideo(float? rating,
                                                    string subtitle,
                                                    string? thumbnaillink,
                                                    string? channelname,
                                                    string srcid,
                                                    string title,
                                                    int learntcount,
                                                    string description,
                                                    int level,
                                                    bool premium)
        {
            var response = await _mediator.Send(new AddVideoCommand(rating, subtitle, thumbnaillink, channelname, srcid, title, learntcount, description, level, premium));
            if (!response.Error) return Ok(response.Result);
            else
            {
                var ErrorResponse = new BaseResponse<Exception>
                {
                    Exception = response.Exception,
                    Message = response.Message
                };
                return new ErrorHandling<Exception>(ErrorResponse);
            }
        }

        [Route("CreateNewVideoV2")]
        [HttpPost]
        public async Task<IActionResult> CreateVideoV2(CreateVideoCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Error) return Ok(response.Result);
            else
            {
                var ErrorResponse = new BaseResponse<Exception>
                {
                    Exception = response.Exception,
                    Message = response.Message
                };
                return new ErrorHandling<Exception>(ErrorResponse);
            }
        }

        [Route("AddVideoGenre")]
        [HttpPut]
        public async Task<IActionResult> AddVideoGenre(UpdateVideoGenreCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Error) return Ok(response.Result);
            else
            {
                var ErrorResponse = new BaseResponse<Exception>
                {
                    Exception = response.Exception,
                    Message = response.Message
                };
                return new ErrorHandling<Exception>(ErrorResponse);
            }
        }

        // PUT api/<VideoController>/5
        [Route("UpdateVideo")]
        [HttpPut]
        public async Task<IActionResult> UpdateVideo(UpdateVideoCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Error) return Ok(response.Result);
            else
            {
                var ErrorResponse = new BaseResponse<Exception>
                {
                    Exception = response.Exception,
                    Message = response.Message
                };
                return new ErrorHandling<Exception>(ErrorResponse);
            }
        }

            // DELETE api/<VideoController>/5
            [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
