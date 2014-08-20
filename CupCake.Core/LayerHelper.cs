using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CupCake.Core.Events;

namespace CupCake.Core
{
    public static class LayerHelper
    {
        public static void LoadEventhandlers(object baseObj, EventManager events, IEnumerable<MethodInfo> methods)
        {
            var eventHandlers = methods.Where(prop => prop.IsDefined(typeof(EventListenerAttribute), true));
            foreach (var eventHandler in eventHandlers)
            {
                if (eventHandler.ReturnType != typeof(void))
                    throw GetEventEx(baseObj, eventHandler.Name, "Event listeners must have the return type void.");
                var parameters = eventHandler.GetParameters();
                if (parameters.Length < 1)
                    throw GetEventEx(baseObj, eventHandler.Name, "Too few arguments.");
                if (parameters.Length > 2)
                    throw GetEventEx(baseObj, eventHandler.Name, "Too many arguments.");

                var e = parameters.Last();
                if (!typeof(Event).IsAssignableFrom(e.ParameterType))
                    throw GetEventEx(baseObj, eventHandler.Name, "Last argument must be an event.");
                var eType = e.ParameterType;

                var bindMethod =
                    typeof(LayerHelper).GetMethod("Bind", BindingFlags.NonPublic | BindingFlags.Static)
                        .MakeGenericMethod(eType);
                var attribute =
                    (EventListenerAttribute)
                        eventHandler.GetCustomAttributes(typeof(EventListenerAttribute), false).First();
                bindMethod.Invoke(null, new object[] {baseObj, events, eventHandler, parameters, attribute.Priority});
            }
        }


        private static void Bind<TEvent>(object baseObj, EventManager events, MethodInfo eventHandler, ParameterInfo[] parameters, EventPriority priority) where TEvent : Event
        {
            EventHandler<TEvent> handler;
            if (parameters.Length == 2)
            {
                var sender = parameters.First();

                if (sender.ParameterType == typeof(object))
                    throw GetEventEx(baseObj, eventHandler.Name, "First argument must be an object.");

                handler =
                    (EventHandler<TEvent>)Delegate.CreateDelegate(typeof(EventHandler<TEvent>), baseObj, eventHandler);
            }
            else
            {
                var tempHandler = (Action<TEvent>)Delegate.CreateDelegate(typeof(Action<TEvent>), baseObj, eventHandler);
                handler = (se, ev) => tempHandler(ev);
            }

            MethodInfo method = typeof(EventManager).GetMethod("Bind").MakeGenericMethod(typeof(TEvent));
            method.Invoke(events, new object[] {handler, priority});
        }

        private static Exception GetEventEx(object baseObj, string name, string reason)
        {
            return
                new TypeLoadException(String.Format("Unable to assign the method {0}.{1} to an event listener. {2}",
                    baseObj.GetType().FullName, name, reason));
        }

        public static string FindName(Type type)
        {
            var pluginName =
                (PluginNameAttribute)
                    Assembly.GetAssembly(type)
                        .GetCustomAttributes(typeof(PluginNameAttribute), false)
                        .FirstOrDefault();

            return pluginName != null
                ? pluginName.Name
                : type.Namespace;
        }
    }
}
