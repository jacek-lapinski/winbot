﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Ninject.Infrastructure.Language;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Winbot.Entities
{
    [Serializable]
    public class Scenario : Entity
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        [XmlArrayItem(typeof(KeyDown), ElementName = nameof(KeyDown))]
        [XmlArrayItem(typeof(KeyUp), ElementName = nameof(KeyUp))]
        [XmlArrayItem(typeof(MouseClick), ElementName = nameof(MouseClick))]
        [XmlArrayItem(typeof(MouseDoubleClick), ElementName = nameof(MouseDoubleClick))]
        [XmlArrayItem(typeof(MouseDown), ElementName = nameof(MouseDown))]
        [XmlArrayItem(typeof(MouseUp), ElementName = nameof(MouseUp))]
        [XmlArrayItem(typeof(MouseMove), ElementName = nameof(MouseMove))]
        [XmlArrayItem(typeof(MouseWheel), ElementName = nameof(MouseWheel))]
        [NewItemTypes(typeof(KeyDown), typeof(KeyUp), typeof(MouseClick), typeof(MouseDoubleClick), typeof(MouseDown), typeof(MouseUp), typeof(MouseMove), typeof(MouseWheel))]
        public List<UserAction> Actions { get; set; }

        public virtual IEnumerable<UserAction> GetExecutingActions()
        {
            return Actions.ToEnumerable();
        }
    }
}
