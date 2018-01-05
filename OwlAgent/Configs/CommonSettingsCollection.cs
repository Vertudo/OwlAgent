using System.Configuration;

namespace OwlAgent.Configs
{
    [ConfigurationCollection(typeof(CommonSettingElement))]
    public class CommonSettingsCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new CommonSettingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CommonSettingElement)(element)).CounterIntervalRead;
        }

        public CommonSettingElement this[int idx]
        {
            get { return (CommonSettingElement)BaseGet(idx); }
        }

    }
}
