using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _01_Routing
{
    [Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET <ValuesController>
        [Route("list")]
        [Route("")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET <ValuesController>/5
        [HttpGet("{id:int}")]
        public string Get(int id)
        {
            return $"value{id}";
        }
    }
}
