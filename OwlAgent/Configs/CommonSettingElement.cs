using System.Configuration;

namespace OwlAgent.Configs
{
    public class CommonSettingElement : ConfigurationElement
    {
        [ConfigurationProperty("CounterIntervalRead", DefaultValue = 5, IsKey = true, IsRequired = true)]
        public int CounterIntervalRead
        {
            get { return ((int)(base["CounterIntervalRead"])); }
            set { base["CounterIntervalRead"] = value; }
        }

        [ConfigurationProperty("FileIntervalWrite", DefaultValue = 5, IsKey = false, IsRequired = true)]
        public int FileIntervalWrite
        {
            get { return ((int)(base["FileIntervalWrite"])); }
            set { base["FileIntervalWrite"] = value; }
        }

        [ConfigurationProperty("SendIntervalData", DefaultValue = 5, IsKey = false, IsRequired = true)]
        public int SendIntervalData
        {
            get { return ((int)(base["SendIntervalData"])); }
            set { base["SendIntervalData"] = value; }
        }
    }
}
