using System;
using System.ServiceProcess;

namespace OwlAgent
{
    public class Program
    {
        public const string ServiceName = "OwlAgent";        

        public static void Main(string[] args)
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

        }

        public static void Stop()
        {
               
        }
    }
}
