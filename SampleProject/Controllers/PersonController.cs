using ASP.NETWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NETWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // Simulated in-memory data storage
        private static readonly List<PersonInfo> people = new List<PersonInfo>();

        /// <summary>
        /// Basic GET endpoint to test API availability
        /// </summary>
        [HttpGet]
        public IActionResult SimpleGet()
        {
            return Ok("Hello, World!");
        }

        /// <summary>
        /// Returns greeting using name from route
        /// Example: GET /api/hello/John
        /// </summary>
        [HttpGet("{name}")]
        public IActionResult GreetFromRoute(string name)
        {
            return Ok($"Hello, {name}!");
        }

        /// <summary>
        /// Receives data from body using POST
        /// Example: POST /api/hello/creatingthroughbody
        /// </summary>
        [HttpPost("creatingthroughbody")]
        public IActionResult CreateFromBody([FromBody] PersonInfo person)
        {
            people.Add(new PersonInfo { Name = person.Name, Age = person.Age });
            return Ok($"Hello, {person.Name}!");
        }

        /// <summary>
        /// Gets data from query string
        /// Example: GET /api/hello/fromquery?name=John
        /// </summary>
        [HttpGet("fromquery")]
        public IActionResult GreetFromQuery([FromQuery] string name)
        {
            return Ok($"Hello, {name}!");
        }

        /// <summary>
        /// Adds data with name from route and age from query string
        /// Example: GET /api/hello/addingdataurlandquery/John?age=25
        /// </summary>
        [HttpGet("addingdataurlandquery/{name}")]
        public IActionResult AddFromUrlAndQuery(string name, [FromQuery] string age)
        {
            people.Add(new PersonInfo { Name = name, Age = int.Parse(age) });
            return Ok($"Data added for {name}!");
        }

        /// <summary>
        /// Updates full record - name from route, age from body
        /// Example: PUT /api/hello/update/John
        /// </summary>
        [HttpPut("update/{name}")]
        public IActionResult Update(string name, [FromBody] PersonInfo updatedPerson)
        {
            var person = people.FirstOrDefault(p => p.Name == name);
            if (person == null) return NotFound("Person not found.");

            person.Age = updatedPerson.Age;
            return Ok(person);
        }

        /// <summary>
        /// Partially updates person age using PATCH
        /// Example: PATCH /api/hello/John
        /// </summary>
        [HttpPatch("{name}")]
        public IActionResult UpdatePartial(string name, [FromBody] PersonInfo partialUpdate)
        {
            var person = people.FirstOrDefault(p => p.Name == name);
            if (person == null) return NotFound("Person not found.");

            if (partialUpdate.Age != 0)
                person.Age = partialUpdate.Age;

            return Ok(person);
        }

        /// <summary>
        /// Deletes a person from the list
        /// Example: DELETE /api/hello/John
        /// </summary>
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var person = people.FirstOrDefault(p => p.Name == name);
            if (person == null) return NotFound("Person not found.");

            people.Remove(person);
            return Ok($"{name} deleted.");
        }
    }
}
