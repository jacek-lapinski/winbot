﻿using System;
using LiteDB;

namespace Winbot.Entities
{
    [Serializable]
    internal abstract class UserAction
    {
        public TimeSpan Time { get; set; }

        [BsonIgnore]
        public abstract string Description { get; }

        public override string ToString()
        {
            return $"{Time}: {Description}";
        }
    }
}
