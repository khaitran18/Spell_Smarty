﻿using MediatR;
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
        public async Task<ActionResult> GetVideos()
        {
            var videos = await _mediator.Send(new GetVideosQuery());
            return Ok(videos);
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
        public async Task<ActionResult> CreateGenre(string genreName)
        {
            var returnGenre = await _mediator.Send(new AddGenreCommand(genreName));
            return Ok(returnGenre);
        }

        [Route("CreateNewVideo")]
        [HttpPost]
        public async Task<ActionResult> CreateVideo(float? rating,
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
            var returnGenre = await _mediator.Send(new AddVideoCommand(rating, subtitle, thumbnaillink, channelname, srcid, title, learntcount, description, level, premium));
            return Ok(returnGenre);
        }

        [Route("CreateNewVideoV2")]
        [HttpPost]
        public async Task<ActionResult> CreateVideoV2(CreateVideoCommand command)
        {
            var returnGenre = await _mediator.Send(command);
            return Ok(returnGenre);
        }

        [Route("AddVideoGenre")]
        [HttpPut]
        public async Task<ActionResult> AddVideoGenre(UpdateVideoGenreCommand command)
        {
            var returnGenre = await _mediator.Send(command);
            return Ok(returnGenre);
        }

        // PUT api/<VideoController>/5
        [Route("UpdateVideo")]
        [HttpPut]
        public async Task<ActionResult> UpdateVideo(UpdateVideoCommand command)
        {
            var returnGenre = await _mediator.Send(command);
            return Ok(returnGenre);
        }

        // DELETE api/<VideoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
