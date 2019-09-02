﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoAppApi.Model
{
    public class ToDo
    {
        [BsonId]
        // standard BSonId generated by MongoDb
        public ObjectId InternalId { get; set; }

        // external Id, easier to reference: 1,2,3.
        public int Id { get; set; }

        public string Body { get; set; } = string.Empty;

        [BsonDateTimeOptions]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public bool Done { get; set; } = false;
    }
}
