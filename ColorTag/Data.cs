using LiteDB;
using System;
using System.Collections.Generic;

namespace ColorTag
{
    public class Data
    {
        [Serializable]
        public class PlayerInfo
        {
            [BsonId]
            public string UserId { get; set; }
            public List<string> Colors { get; set; } 
        }
    }
}