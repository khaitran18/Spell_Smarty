using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpellSmarty.Application.Commands;
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
            var video = await _mediator.Send(new GetSingleVideoQuery(videoId,Authorization));
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

        // POST api/<VideoController>
        [Route("SaveProgress")]
        [HttpPost]
        public async Task<ActionResult> SaveProgress([FromHeader] string? Authorization, int videoId, int progress)
        {
            string returnPrg = await _mediator.Send(new SaveProgressQuery(Authorization, videoId, progress));
            return Ok(returnPrg);
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

        // PUT api/<VideoController>/5
        [Route("UpdateVideo")]
        [HttpPut("{id}")]
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
