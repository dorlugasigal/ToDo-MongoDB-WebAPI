using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ToDoAppApi.Interfaces;
using ToDoAppApi.Model;

namespace ToDoAppApi.Data
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoContext _context = null;

        public ToDoRepository(IOptions<Settings> settings)
        {
            _context = new ToDoContext(settings);
        }

        public async Task<IEnumerable<ToDo>> GetAllToDo()
        {
            try
            {
                return await _context.ToDos.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<ToDo> GetToDo(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.ToDos
                                .Find(note => note.Id == id || note.InternalId == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after body text, updated time, and header image size
        //
        public async Task<IEnumerable<ToDo>> GetToDo(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            try
            {
                var query = _context.ToDos.Find(note => note.Body.Contains(bodyText) &&
                                       note.CreatedOn >= updatedFrom);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Try to convert the Id to a BSonId value
        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddToDo(ToDo item)
        {
            try
            {
                await _context.ToDos.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveToDo(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.ToDos.DeleteOneAsync(
                     Builders<ToDo>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateToDo(string id, string body)
        {
            var filter = Builders<ToDo>.Filter.Eq(s => s.Id, id);
            var update = Builders<ToDo>.Update
                            .Set(s => s.Body, body)
                            .CurrentDate(s => s.CreatedOn);

            try
            {
                UpdateResult actionResult = await _context.ToDos.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateToDo(string id, ToDo item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.ToDos
                                                .ReplaceOneAsync(n => n.Id.Equals(id)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<bool> UpdateToDoDocument(string id, string body)
        {
            var item = await GetToDo(id) ?? new ToDo();
            item.Body = body;
            item.CreatedOn = DateTime.Now;

            return await UpdateToDo(id, item);
        }

        public async Task<bool> RemoveAllToDos()
        {
            try
            {
                DeleteResult actionResult = await _context.ToDos.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // it creates a sample compound index (first using UserId, and then Body)
        // 
        // MongoDb automatically detects if the index already exists - in this case it just returns the index details
        public async Task<string> CreateIndex()
        {
            try
            {
                IndexKeysDefinition <ToDo> keys = Builders<ToDo>
                                                    .IndexKeys
                                                    .Ascending(item => item.Body);

                return await _context.ToDos
                                .Indexes.CreateOneAsync(new CreateIndexModel<ToDo>(keys));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
