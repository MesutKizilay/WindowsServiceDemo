using System;
using System.ServiceProcess;
using System.Timers;
using WindowsServiceDemo.DataAccess;
using WindowsServiceDemo.Entities;
using WindowsServiceDemo.Helper;

namespace WindowsServiceDemo
{
    public partial class Service1 : ServiceBase
    {
        private readonly ICarDal _carDal;

        private Timer _timer;

        public Service1(ICarDal carDal)
        {
            InitializeComponent();
            _carDal = carDal;
        }

        protected override void OnStart(string[] args)
        {
            LogHelper.LogMessage("OnStart");
            try
            {
                if (_timer == null)
                {
                    _timer = new Timer();
                }

                Timer_Elapsed(null, null);
                _timer.Interval = 10000;
                _timer.Enabled = true;
                _timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
                _timer.Start();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                //throw;
            }
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LogHelper.LogMessage("Timer_Elapsed");

            try
            {
                _timer.Stop();
                _carDal.Add(new Car() { Name = "aaa" });
                _timer.Start();

            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                _timer.Start();
                //throw;
            }
        }

        protected override void OnStop()
        {
            LogHelper.LogMessage("OnStop");
        }
    }
}