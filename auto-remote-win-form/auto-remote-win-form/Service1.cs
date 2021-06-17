using selenium_remote;
using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Timers;

namespace auto_remote_win_form
{
    public partial class Service1 : ServiceBase
    {
        private static System.Timers.Timer aTimer;
        private System.Diagnostics.EventLog eventLog1;
        public Service1()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.Source = "CheckSource";
            eventLog1.Log = "MyNewLog";
            if (!System.Diagnostics.EventLog.SourceExists("NghiemLog"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "CheckSource", "MyNewLog");
            }
            
            try
            {
                eventLog1.WriteEntry("Onstart ok");
                SetTimer();
            }
            catch(Exception ex)
            {
                eventLog1.WriteEntry("In OnStart."+ex.Message);
            }
            

        }

        protected override void OnStop()
        {
        }
        private static void SetTimer()
        {
            aTimer = new System.Timers.Timer(6000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Robot robot = new Robot("");
            robot.GoToPage("https://www.google.com/");
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
    }
}
