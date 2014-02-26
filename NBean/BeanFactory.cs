using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NBean
{
    public class BeanFactory
    {
        private static readonly XmlSchemaSet schemas;

        static BeanFactory()
        {
            schemas = new XmlSchemaSet();

            using (var stream = typeof(BeanFactory).Assembly.GetManifestResourceStream(typeof(BeanFactory), "appContext.xsd"))
            {
                schemas.Add(XmlSchema.Read(stream, null));
            }
        }

        private AppContext appContext;
        private Dictionary<string, object> singletonBeans = new Dictionary<string, object>();

        public BeanFactory()
            : this("appContext.xml", Assembly.GetCallingAssembly(), true)
        {
        }

        public BeanFactory(string classPath, Assembly assembly, bool prependAssemblyName)
            : this(OpenClassPath(classPath, assembly, prependAssemblyName))
        {
        }

        public BeanFactory(Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                CloseInput = true,
                ValidationType = ValidationType.Schema,
                Schemas = schemas
            };

            XmlSerializer serializer = new XmlSerializer(typeof(AppContext));

            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                this.appContext = (AppContext)serializer.Deserialize(reader);
            }
        }

        public T Get<T>(string beanId) where T : class
        {
            return (T)this.Get(beanId);
        }

        public object Get(string beanId)
        {
            Bean bean = this.appContext.Items.First(b => b.id == beanId);

            if (bean.singleton)
            {
                object instance;

                if (!this.singletonBeans.TryGetValue(bean.id, out instance))
                {
                    this.singletonBeans.Add(bean.id, instance = this.CreateInstance(bean));
                }

                return instance;
            }

            return this.CreateInstance(bean);
        }

        private object CreateInstance(Bean bean)
        {
            Type t = Type.GetType(bean.@class);
            List<object> args = new List<object>();

            if (bean.constructorarg != null)
            {
                args.AddRange(bean.constructorarg.Select(p => p.value != null ? p.value : this.Get(p.Item.bean)));
            }

            return t.InvokeMember(null, BindingFlags.Instance | BindingFlags.CreateInstance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic, Type.DefaultBinder, null, args.ToArray());
        }

        private static Stream OpenClassPath(string classPath, Assembly assembly, bool prependAssemblyName)
        {
            if (prependAssemblyName)
            {
                classPath = assembly.GetName().Name + "." + classPath;
            }

            return assembly.GetManifestResourceStream(classPath);
        }
    }
}
