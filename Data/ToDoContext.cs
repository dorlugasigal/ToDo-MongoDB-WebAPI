using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoAppApi.Model;

namespace ToDoAppApi.Data
{
    public class ToDoContext
    {
        private readonly IMongoDatabase _database = null;

        public ToDoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<ToDo> ToDos => _database.GetCollection<ToDo>("ToDo");
    }
}
