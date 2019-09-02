using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ToDoAppApi.Infrastructure;
using ToDoAppApi.Interfaces;
using ToDoAppApi.Model;

namespace ToDoAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ToDosController : Controller
    {
        private readonly IToDoRepository _todoRepository;

        public ToDosController(IToDoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<ToDo>> Get()
        {
            return await _todoRepository.GetAllToDo();
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public async Task<ToDo> Get(string id)
        {
            return await _todoRepository.GetToDo(id) ?? new ToDo();
        }

        // GET api/notes/text/date/size
        // ex: http://localhost:53617/api/notes/Test/2018-01-01/10000
        [NoCache]
        [HttpGet(template: "{bodyText}/{updatedFrom}/{headerSizeLimit}")]
        public async Task<IEnumerable<ToDo>> Get(string bodyText, 
                                                 DateTime updatedFrom, 
                                                 long headerSizeLimit)
        {
            return await _todoRepository.GetToDo(bodyText, updatedFrom, headerSizeLimit) 
                        ?? new List<ToDo>();
        }

        // POST api/notes
        [HttpPost]
        public void Post([FromBody] ToDoParam newToDo)
        {
            _todoRepository.AddToDo(new ToDo
                                        {
                                            Id = newToDo.Id,
                                            Body = newToDo.Body,
                                            CreatedOn = DateTime.Now
                                        });
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _todoRepository.UpdateToDoDocument(id, value);
        }

        // DELETE api/notes/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _todoRepository.RemoveToDo(id);
        }
    }
}
