using System;
using System.Threading.Tasks;
using API.Controllers.Requests;
using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ILogger<ArticleController> _logger;

        private readonly IMediator _mediatr;

        public ArticleController(ILogger<ArticleController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var response = _mediatr.Send(new GetArticlesQuery());
            return Ok(response.Result);
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            IActionResult result;
            try
            {
                var query = new GetArticleQuery(id);
                var response = _mediatr.Send(query);
                result = Ok(response.Result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                result = BadRequest();
            }

            return result;
        }

        [HttpPost]
        public IActionResult Post(AddArticleRequest request)
        {
            IActionResult result;
            try
            {
                var command = new AddArticleCommand(request.Content, request.Description);
                var response =_mediatr.Send(command);
                result = (response.Status == TaskStatus.Faulted) ? BadRequest(response.Exception.Message) : Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                result = BadRequest(e.Message);
            }

            return result;
        }
        
        [HttpPut]
        [Route("{id}/like")]
        public IActionResult Put(long id)
        {
            IActionResult result;
            try
            {
                var command = new LikeArticleCommand(id);
                var response = _mediatr.Send(command);
                result = (response.Status == TaskStatus.Faulted) ? BadRequest(response.Exception.Message) : Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                result = BadRequest();
            }

            return result;
        }
    }
}