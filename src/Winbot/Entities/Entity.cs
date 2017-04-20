using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LiteDB;

namespace Winbot.Entities
{
    [Serializable]
    public abstract class Entity : ICloneable
    {
        [BsonId]
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        #region Cloneable
        public object Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return formatter.Deserialize(ms);
            }
        }
        #endregion
    }
}
