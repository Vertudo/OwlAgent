using System;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using OwlAgent.DataWriting;

namespace OwlAgent
{
    class Program
    {
        public const string ServiceName = "OwlAgent";

        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
            {
                using (var service = new Service())
                    ServiceBase.Run(service);
            }
            else {
                Start(args);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                Stop();
            }
        }

        public static void Start(string[] args)
        {
            Config conf = new Config();
            var countersConfig = conf.GetCounters();   
            
            if (countersConfig.Count == 0)
            {
                Console.WriteLine("В файле настроек не определены счетчики.");
                return;
            }

            DataWriter writer = new DataWriter(conf.FileIntervalWrite(), conf.SendIntervalData());

            List<PerformanceCounter> perfCounters = new List<PerformanceCounter>(); 
            foreach (Config.Counter counterInfo in countersConfig)
            {
                perfCounters.Add(new PerformanceCounter(counterInfo.Category, counterInfo.CounterName, counterInfo.Instance));
            }            

            while (true)
            {
                Thread.Sleep(1000 * conf.CounterIntervalRead());

                foreach (PerformanceCounter perfCounter in perfCounters)
                {
                    double counterValue = perfCounter.NextValue();                    
                    writer.AppendData(perfCounter, counterValue);
                }

            }
        }

        public static void Stop()
        {

        }
    }
}
