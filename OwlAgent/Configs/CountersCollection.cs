using System.Configuration;

namespace OwlAgent.Configs
{
    [ConfigurationCollection(typeof(CounterElement))]
    public class CountersCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new CounterElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CounterElement)(element)).CounterName;
        }

        public CounterElement this[int idx]
        {
            get { return (CounterElement)BaseGet(idx); }
        }

    }
}
