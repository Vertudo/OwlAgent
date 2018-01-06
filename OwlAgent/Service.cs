using System;
using System.ServiceProcess;
using System.Threading;
using OwlAgent.DataWriting;
using System.Collections.Generic;
using System.Diagnostics;

namespace OwlAgent
{

    public class Service : ServiceBase
    {
        private Thread _thread;
        private volatile bool _keepGoing = true;
        private string eventSource;

        public Service()
        {
            InitializeComponent();

            eventSource = ServiceName;

            if (!EventLog.SourceExists(eventSource))
            {
                EventLog.CreateEventSource(eventSource, "Applicaiton");
            }
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(eventSource, "Запущена служба OwlAgent.");

            _thread = new Thread(new ThreadStart(readCounterValue));
            _thread.Start();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry(eventSource, "Остановлена служба OwlAgent.");
            _keepGoing = false;
            _thread.Join();
        }

        private void InitializeComponent()
        {
            // 
            // Service
            // 
            this.CanPauseAndContinue = true;
            this.ServiceName = "OwlAgent";

        }

        private void readCounterValue()
        {
            Config conf = new Config();
            var countersConfig = conf.GetCounters();

            if (countersConfig.Count == 0)
            {
                EventLog.WriteEntry(eventSource, "В файле конфигурации не определены счетчики.");
                this.Stop();
            }

            DataWriter writer = new DataWriter(conf.FileIntervalWrite(), conf.SendIntervalData());

            List<PerformanceCounter> perfCounters = new List<PerformanceCounter>();
            foreach (Config.Counter counterInfo in countersConfig)
            {
                perfCounters.Add(new PerformanceCounter(counterInfo.Category, counterInfo.CounterName, counterInfo.Instance));
            }

            while (_keepGoing)
            {
                foreach (PerformanceCounter perfCounter in perfCounters)
                {
                    double counterValue = perfCounter.NextValue();
                    writer.AppendData(perfCounter, counterValue);
                }

                Thread.Sleep(1000 * conf.CounterIntervalRead());
            }
        }
    }
}