using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwlAgent.DataWriting
{
    public class DataWriter
    {
        private int fileIntervalWrite;
        private int fileIntervalCreateNew;

        private DateTime nextDataWrite;
        private DateTime nextCreateNewFile;

        private string pathToLogsDirectory;
        private string currentDataFile;

        [DataContract]
        public struct LogData
        {
            private DateTime date;
            private string categoryName;
            private string counterName;
            private string instance;
            private double counterValue;

            public LogData(DateTime date, string categoryName, string counterName, string instance, double counterValue)
            {
                this.date = date;
                this.categoryName = categoryName;
                this.counterName = counterName;
                this.instance = instance;
                this.counterValue = counterValue;
            }

            [DataMember]
            public DateTime Date { get => date; set => date = value; }
            [DataMember]
            public string CategoryName { get => categoryName; set => categoryName = value; }
            [DataMember]
            public string CounterName { get => counterName; set => counterName = value; }
            [DataMember]
            public string Instance { get => instance; set => instance = value; }
            [DataMember]
            public double CounterValue { get => counterValue; set => counterValue = value; }
        }

        private List<LogData> DataForWriting;

        public DataWriter(int fileIntervalWrite, int fileIntervalCreateNew)
        {
            this.fileIntervalWrite = fileIntervalWrite;
            this.fileIntervalCreateNew = fileIntervalCreateNew;

            string pathToApplication = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.pathToLogsDirectory = pathToApplication + "\\logs";
            Directory.CreateDirectory(pathToLogsDirectory);
        }

        public void AppendData(PerformanceCounter counter, double counterValue)
        {
            DateTime localData = DateTime.Now;

            DataWrite(localData);
            CreateFileAndSendData(localData);

            DataForWriting.Add(new LogData(localData, counter.CategoryName, counter.CounterName, counter.InstanceName, counterValue));            
        }

        private void DataWrite(DateTime localData)
        {
            if (nextDataWrite == default(DateTime) || nextDataWrite <= localData)
            {
                if (DataForWriting != null && DataForWriting.Count > 0)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    using (StreamWriter writer = File.AppendText(currentDataFile))
                    {
                        foreach (LogData data in DataForWriting)
                        {
                            string json = serializer.Serialize(data);
                            writer.WriteLine(json);
                        }
                        writer.Close();
                    }
                }

                nextDataWrite = localData.AddSeconds(fileIntervalWrite);
                DataForWriting = new List<LogData>();
            }
        }

        private void CreateFileAndSendData(DateTime localData)
        {
            if (nextCreateNewFile == default(DateTime) || nextCreateNewFile <= localData)
            {
                if (nextCreateNewFile != default(DateTime))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    using (StreamReader reader = File.OpenText(currentDataFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            LogData data = serializer.Deserialize<LogData>(line);
                        }
                        reader.Close();                        
                    }
                    File.Delete(currentDataFile);
                }
                nextCreateNewFile = localData.AddSeconds(fileIntervalCreateNew);
                currentDataFile = pathToLogsDirectory + "\\datafile_" + nextCreateNewFile.ToString("ddMMyyyyHmmss") + ".data";
                File.Create(currentDataFile).Close();
            }
        }
    }
}

