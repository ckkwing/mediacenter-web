using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Service.DataAccess.Entity;
using Web.Service.DataAccess.Interface;
using Web.Service.Extensions;
using Web.Service.RequestModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoDao _videoDao;

        public VideoController(IVideoDao videoDao)
        {
            _videoDao = videoDao;
        }

        // GET: api/<VideoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VideoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //GET: api/<VideoController>/GetVideosByPaging?PageIndex=1&TotalCount=3
        [HttpGet("GetVideosByPaging")]
        public IEnumerable<Video> GetVideosByPaging([FromQuery] PagingVideosRequestModel pagingVideosRequestModel)
        {
            return _videoDao.GetVideosByPaging(pagingVideosRequestModel.PageIndex, pagingVideosRequestModel.TotalCount) ?? new List<Video>();
        }

        // POST api/<VideoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VideoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VideoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return _videoDao.DeleteById(id, false) ? Ok() : this.InternalServerError();
        }
    }
}
