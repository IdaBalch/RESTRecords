using Microsoft.AspNetCore.Mvc;
using RESTRecords.Models;
using RESTRecords.Repos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTRecords.Controllers
{    

    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {

        private readonly RecordsRepositoryList repo;

        public RecordsController(RecordsRepositoryList repo)
        {
            this.repo = repo;
        }

        // GET: api/<RecordsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Records>> GetAll()
        {
            IEnumerable<Records> records = repo.GetAllRecords();
            if (records.Any())
            {
                return Ok(records);
            }
            else
            {
                return NotFound();
            }
        }

        //// GET api/<RecordsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<RecordsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RecordsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RecordsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
