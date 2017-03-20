using System;
using System.Collections.Generic;
using System.Linq;
using Winbot.Entities;

namespace Winbot.Listeners
{
    internal class ScenarioBuildListener : IUserActionListener
    {
        private readonly IList<UserAction> _actions;

        public ScenarioBuildListener()
        {
            _actions = new List<UserAction>();
        }

        void IUserActionListener.Update(UserAction userAction)
        {
            _actions.Add(userAction);
        }

        public Scenario Build(string name)
        {
            var scenario = new Scenario()
            {
                Name = name,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
                Actions = _actions.ToArray()
            };
            return scenario;
        }
    }
}
