using System;
using Microsoft.AspNetCore.Mvc;
using ToDoAppApi.Interfaces;
using ToDoAppApi.Model;

namespace ToDoAppApi.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly IToDoRepository _todoRepository;

        public SystemController(IToDoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // Call an initialization - api/system/init
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting != "init") return "Unknown";
            _todoRepository.RemoveAllToDos();
            var name = _todoRepository.CreateIndex();

            _todoRepository.AddToDo(new ToDo()
            {
                Id = 1,
                Body = "Test note 1",
                CreatedOn = DateTime.Now,
                Done = false
            });

            _todoRepository.AddToDo(new ToDo()
            {
                Id = 2,
                Body = "Test note 2",
                CreatedOn = DateTime.Now,
                Done = false
            });

            _todoRepository.AddToDo(new ToDo()
            {
                Id = 3,
                Body = "Test note 3",
                CreatedOn = DateTime.Now,
                Done = false
            });

            _todoRepository.AddToDo(new ToDo()
            {
                Id = 4,
                Body = "Test note 4",
                CreatedOn = DateTime.Now,
                Done = false
            });
            return "Database ToDoDb was created, and collection 'ToDo' was filled with 4 sample items";
        }
    }
}
