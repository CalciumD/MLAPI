using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using System.Data;
using System.Xml.Linq;

namespace MLAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineLearningController : ControllerBase
    {

        private readonly ILogger<MachineLearningController> _logger;

        public MachineLearningController(ILogger<MachineLearningController> logger)
        {
            _logger = logger;
        }


        //[HttpGet(Name = "CalculatingResultForIssue")]
        //public ActionResult Get(string issue)
        //{
        //    string res = GetSolution(issue);
        //    return "d";
        //}

        [HttpPost]
        public IActionResult Post([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be empty.");
            }

            // Process the 'name' parameter as needed
            var result = $"Hello, {name}!";
            return Ok(result);
        }

    }
}
