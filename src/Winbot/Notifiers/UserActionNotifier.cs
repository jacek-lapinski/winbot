using System;
using System.Collections.Generic;
using Winbot.Entities;
using Winbot.Listeners;

namespace Winbot.Notifiers
{
    internal abstract class UserActionNotifier
    {
        protected readonly HashSet<IUserActionListener> Listeners;

        protected UserActionNotifier()
        {
            Listeners = new HashSet<IUserActionListener>();
        }

        public void Subscribe(IUserActionListener listener)
        {
            Listeners.Add(listener);
        }

        public void Unsubscribe(IUserActionListener listener)
        {
            Listeners.Remove(listener);
        }

        protected void Notify(UserAction userAction)
        {
            foreach (var listener in Listeners)
            {
                listener.Update(userAction);
            }
        }

        public abstract void Start(DateTime referenceStartTime);
        public abstract void Stop();
    }
}
