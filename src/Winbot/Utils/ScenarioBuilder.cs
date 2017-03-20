using System;
using System.Collections.Generic;
using System.Linq;
using Winbot.Entities;
using Winbot.Listeners;
using Winbot.Notifiers;
using Winbot.Settings;

namespace Winbot.Utils
{
    internal class ScenarioBuilder : IScenarioBuilder
    {
        private readonly AppSettings _appSettings;
        private ScenarioBuildListener _scenarioBuildListener;

        private IEnumerable<UserActionNotifier> SelectedNotifiers
            => _appSettings.Notifiers.Where(n => n.Selected).Select(n => n.Notifier);

        public ScenarioBuilder(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Init()
        {
            _scenarioBuildListener = new ScenarioBuildListener();
            SubscribeScenarioBuildListener();
            StartBuilding();
        }

        public Scenario Build()
        {
            StopBuilding();
            UnsubscribeScenarioBuildListener();

            //todo get name
            var name = Guid.NewGuid().ToString();

            var scenario = _scenarioBuildListener.Build(name);
            return scenario;
        }

        private void SubscribeScenarioBuildListener()
        {
            foreach (var notifier in SelectedNotifiers)
            {
                notifier.Subscribe(_scenarioBuildListener);
            }
        }

        private void UnsubscribeScenarioBuildListener()
        {
            foreach (var notifier in SelectedNotifiers)
            {
                notifier.Unsubscribe(_scenarioBuildListener);
            }
        }

        private void StartBuilding()
        {
            var now = DateTime.Now;
            foreach (var notifier in SelectedNotifiers)
            {
                notifier.Start(now);
            }
        }

        private void StopBuilding()
        {
            foreach (var notifier in SelectedNotifiers)
            {
                notifier.Stop();
            }
        }
    }
}
