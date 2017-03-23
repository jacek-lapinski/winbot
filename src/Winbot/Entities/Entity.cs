using System;
using LiteDB;

namespace Winbot.Entities
{
    internal abstract class Entity
    {
        [BsonId]
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
