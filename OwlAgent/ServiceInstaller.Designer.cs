namespace OwlAgent
{
    partial class ServiceInstaller
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.owlServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.owlServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // owlServiceProcessInstaller
            // 
            this.owlServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.owlServiceProcessInstaller.Password = null;
            this.owlServiceProcessInstaller.Username = null;
            // 
            // owlServiceInstaller
            //             
            this.owlServiceInstaller.ServiceName = "OwlAgent";
            this.owlServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.owlServiceProcessInstaller,
            this.owlServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller owlServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller owlServiceInstaller;
    }
}