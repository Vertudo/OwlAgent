using System.Configuration;

namespace OwlAgent.Configs
{
    public class OwlConfigsSection : ConfigurationSection
    {

        [ConfigurationProperty("Counters")]
        public CountersCollection CountersItems
        {
            get { return (CountersCollection)(base["Counters"]); }
        }

        [ConfigurationProperty("CommonSettings")]
        public CommonSettingsCollection CommonSettings
        {
            get { return (CommonSettingsCollection)(base["CommonSettings"]); }
        }

    }
}
