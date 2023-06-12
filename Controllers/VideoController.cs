using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetVideosByCreator(string creator)
        {
            var video = await _mediator.Send(new GetVideosByCreatorQuery(creator));
            return Ok(video);
        }
        [Route("GetVideoByGenre")]
        [HttpGet()]
        public async Task<ActionResult> GetVideosByGenre(int genreId)
        {
            var videos = await _mediator.Send(new GetVideosByGenreQuery(genreId));
            return Ok(videos);
        }
        [Route("GetVideoByUserId")]
        [HttpGet()]
        public async Task<ActionResult> GetVideosByUserId([FromHeader] string? Authorization, int userId)
        {
            var videos = await _mediator.Send(new GetVideosByUserIdQuery(Authorization, userId));
            return Ok(videos);
        }

        // POST api/<VideoController>
        [Route("SaveProgress")]
        [HttpPost]
        public async Task<ActionResult> SaveProgress(int videoId, string progress)
        {
            var videos = await _mediator.Send(new SaveProgressQuery(videoId, progress));
            return Ok(videos);
        }

        // PUT api/<VideoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VideoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
