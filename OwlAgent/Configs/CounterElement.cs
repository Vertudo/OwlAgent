using System.Configuration;

namespace OwlAgent.Configs
{
    public class CounterElement : ConfigurationElement
    {

        [ConfigurationProperty("Category", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Category
        {
            get { return ((string)(base["Category"])); }
            set { base["Category"] = value; }
        }

        [ConfigurationProperty("CounterName", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string CounterName
        {
            get { return ((string)(base["CounterName"])); }
            set { base["CounterName"] = value; }
        }

        [ConfigurationProperty("Instance", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Instance
        {
            get { return ((string)(base["Instance"])); }
            set { base["Instance"] = value; }
        }

    }
}
