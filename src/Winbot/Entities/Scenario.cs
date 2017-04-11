using System;
using System.Collections.Generic;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Winbot.Entities
{
    [Serializable]
    internal class Scenario : Entity
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        [NewItemTypes(typeof(KeyDown), typeof(KeyUp), typeof(MouseClick), typeof(MouseDoubleClick))]
        public List<UserAction> Actions { get; set; }
    }
}
