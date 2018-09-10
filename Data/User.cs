using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InamdoMsgSender
{
    public class User
    {

        // not using userId because NewtonSoft cannot deserialize an ObjectId type. Throws an error. Will use code and userPhone for uniqueness.

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId userId { get; set; }

        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string internalId { get; set; }

        public int code { get; set; }
        public string userName { get; set; }
        public string userPhone { get; set; }
        public string userEmail { get; set; }
        public string message { get; set; }
        public bool optIn { get; set; }
    }
}