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

        public Scenario Build()
        {
            var scenario = new Scenario()
            {
                Name = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
                Actions = _actions.ToArray()
            };
            return scenario;
        }
    }
}
