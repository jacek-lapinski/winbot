using System;
using System.Xml.Serialization;
using LiteDB;

namespace Winbot.Entities
{
    [Serializable]
    public abstract class UserAction
    {
        [XmlIgnore]
        public TimeSpan Time { get; set; }

        [XmlElement(ElementName = nameof(Time))]
        [BsonIgnore]
        public string TimeString
        {
            get { return Time.ToString(); }
            set { Time = TimeSpan.Parse(value); }
        }

        [BsonIgnore]
        public abstract string Description { get; }

        public override string ToString()
        {
            return $"{Time}: {Description}";
        }
    }
}
