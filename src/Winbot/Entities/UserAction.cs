using System;

namespace Winbot.Entities
{
    internal abstract class UserAction
    {
        public TimeSpan Time { get; set; }
        public abstract string Description { get; }

        public override string ToString()
        {
            return $"{Time}: {Description}";
        }
    }
}
