using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoAppApi.Model;

namespace ToDoAppApi.Interfaces
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDo>> GetAllToDo();

        Task<ToDo> GetToDo(string id);

        // query after multiple parameters
        Task<IEnumerable<ToDo>> GetToDo(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        // add new note document
        Task AddToDo(ToDo item);

        // remove a single document / note
        Task<bool> RemoveToDo(string id);

        // update just a single document / note
        Task<bool> UpdateToDo(string id, string body);

        // demo interface - full document update
        Task<bool> UpdateToDoDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllToDos();

        // creates a sample index
        Task<string> CreateIndex();
    }
}
