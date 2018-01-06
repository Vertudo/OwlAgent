using System.ServiceProcess;

namespace OwlAgent
{

    public class Service : ServiceBase
    {
        public Service()
        {
            ServiceName = Program.ServiceName;
        }

        protected override void OnStart(string[] args)
        {
            Program.Start(args);
        }

        protected override void OnStop()
        {
            Program.Stop();
        }

        private void InitializeComponent()
        {
            // 
            // Service
            // 
            this.ServiceName = "OwlAgent";

        }
    }
}