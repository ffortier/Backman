using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backman.Services
{
    static class Injector
    {
        private static Dictionary<Type, Dictionary<string, Func<object>>> instances = new Dictionary<Type,Dictionary<string, Func<object>>>();

        public static void Infer<T>(T value) where T : class
        {
            Dictionary<string, Func<object>> dict;

            if (!instances.TryGetValue(typeof(T), out dict))
            {
                dict = new Dictionary<string, Func<object>>();
                instances.Add(typeof(T), dict);
            }

            dict[String.Empty] = () => value;
        }

        public static void Infer<T>(String key, T value) where T : class
        {
            Dictionary<string, Func<object>> dict;

            if (!instances.TryGetValue(typeof(T), out dict))
            {
                dict = new Dictionary<string, Func<object>>();
                instances.Add(typeof(T), dict);
            }

            dict[key] = () => value;
        }

        public static void Infer<T>(Func<T> value) where T : class
        {
            Dictionary<string, Func<object>> dict;

            if (!instances.TryGetValue(typeof(T), out dict))
            {
                dict = new Dictionary<string, Func<object>>();
                instances.Add(typeof(T), dict);
            }

            dict[String.Empty] = value;
        }

        public static void Infer<T>(String key, Func<T> value) where T : class
        {
            Dictionary<string, Func<object>> dict;

            if (!instances.TryGetValue(typeof(T), out dict))
            {
                dict = new Dictionary<string, Func<object>>();
                instances.Add(typeof(T), dict);
            }

            dict[key] = value;
        }

        public static T Get<T>() where T : class
        {
            return (T)instances[typeof(T)][String.Empty]();
        }

        public static T Get<T>(String key) where T : class
        {
            return (T)instances[typeof(T)][key]();
        }

#if DEBUG
        public static void Reset()
        {
            instances.Clear();
        }
#endif
    }
}
