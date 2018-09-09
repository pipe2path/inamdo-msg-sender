using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace InamdoMsgSender
{
    public class User
    {
        //[BsonId]
        //public ObjectId internalId { get; set; }
                
        public int surveyId { get; set; }
        public string userName { get; set; }
        public string userPhone { get; set; }
        public string userEmail { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public bool optIn { get; set; }
    }
}