using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer dispatcherTimer, timeDispatcher, demoDispatcher;
        private int timesTicked = 1;
        private double ProgressAmount = 0;
        private int sec = 0;
        private int min = 0;
        private int hour = 0;

        private DateTime startedTime;
        private TimeSpan timePassed, timeSinceLastStop;


        public MainPage()
        {
            this.InitializeComponent();
        }

        bool isStop = false;
        //starting the timer
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (isStop == false)
            {
                isStop = true;
                startedTime = DateTime.Now;
                DispatcherTimerSetup();

                Start.Content = "Stop";
            }
            else
            {
                isStop = false;
                dispatcherTimer.Stop();
                demoDispatcher.Stop();
                Hour.Text = "00:00:00:00";
                Start.Content = "Start";
                ProgressControl.SetBarLength(0.0);
                ProgressAmount = 0.0;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ProgressControl.SetBarLength(1.0);
        }


        private void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 10);
            dispatcherTimer.Start();



            timeSinceLastStop = TimeSpan.Zero;
            Hour.Text = "00:00:00:00";
            demoDispatcher = new DispatcherTimer();
            demoDispatcher.Tick += DemoDispatcher_Tick;
            demoDispatcher.Interval = new TimeSpan(0, 0, 0, 0, 1);
            demoDispatcher.Start();
        }

        int lapCount = 0;

        private void Lap_Click(object sender, RoutedEventArgs e)
        {
            lapCount++;
            txtLap.Text += "Lap " + lapCount + ": " + Hour.Text + "\n";
        }

        private string MakeDigitString(int number, int count)
        {
            string result = "0";
            if (count == 2)
            {
                if (number < 10)
                    result = "0" + number;
                else
                    result = number.ToString();
            }
            else if (count == 3)
            {
                if (number < 10)
                    result = "00" + number;
                else if (number > 9 && number < 100)
                {
                    result = "0" + number;
                }
                else
                    result = number.ToString();
            }
            return result;
        }

        /*private void DemoDispatcher_Tick(object sender, object e)
        {
            timePassed = DateTime.Now - startedTime;
            Hour.Text = MakeDigitString((timeSinceLastStop + timePassed).Hours, 2) + ":"
                + MakeDigitString((timeSinceLastStop + timePassed).Minutes, 2) + ":"
                + MakeDigitString((timeSinceLastStop + timePassed).Seconds, 2) + ":"
                + MakeDigitString((timeSinceLastStop + timePassed).Milliseconds, 3);
        }*/



        private void dispatcherTimer_Tick(object sender, object e)
        {
            timesTicked++;
            ProgressControl.SetBarLength(ProgressAmount);
            ProgressAmount += (1.0 / 60.0) * (7.95 / 60.0);
            if (ProgressAmount > 1.0)
                ProgressAmount = 0.0;
        }
    }
}