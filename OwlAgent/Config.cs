using System.Collections.Generic;
using System.Configuration;
using OwlAgent.Configs;

namespace OwlAgent
{
    public class Config
    {

        #region Счетчики
        public struct Counter
        {            
            private string category;
            private string counterName;
            private string instance;

            public string Category { get => category; set => category = value; }
            public string CounterName { get => counterName; set => counterName = value; }
            public string Instance { get => instance; set => instance = value; }

            public Counter(string category, string counterName, string instance) {
                this.category = category;
                this.counterName = counterName;
                this.instance = instance;
            }
        }
        private List<Counter> Counters;
        #endregion

        #region Общие настройки
        private int counterIntervalRead = 5;    // Интервал опроса счетчиков, в секундах
        private int fileIntervalWrite = 60;     // Интервал записи данных в файл, в секундах
        private int sendIntervalData = 300;     // Интервал отправки данных и создания нового файла данных, в секундах. Не больше часа (3600)!                                            
        #endregion

        public Config()
        {
            Configuration configManager = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            OwlConfigsSection section = (OwlConfigsSection)configManager.GetSection("OwlConfigs");

            if (section != null)
            {
                Counters = new List<Counter>();
                // Counters
                for (int i = 0; i < section.CountersItems.Count; i++)
                {
                    CounterElement counter = section.CountersItems[i];
                    Counters.Add(new Counter(counter.Category, counter.CounterName, counter.Instance));
                }

                if (section.CommonSettings.Count == 1)
                {
                    CommonSettingElement commonSettings = section.CommonSettings[0];
                    counterIntervalRead = commonSettings.CounterIntervalRead;
                    fileIntervalWrite = commonSettings.FileIntervalWrite;
                    sendIntervalData = commonSettings.SendIntervalData;
                }
            }
        }

        public List<Counter> GetCounters()
        {
            return Counters;
        }

        public int CounterIntervalRead()
        {
            return counterIntervalRead;
        }

        public int FileIntervalWrite()
        {
            return fileIntervalWrite;
        }

        public int SendIntervalData()
        {
            return sendIntervalData;
        }
    }
}
