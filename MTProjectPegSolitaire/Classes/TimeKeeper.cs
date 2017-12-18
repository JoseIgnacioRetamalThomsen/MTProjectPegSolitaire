using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
/*
 * Jose Ignacio Retamal - Triangle Peg Solitaire - Timer
 * 21/11/2017 class made: jose Retamal
 * 10/12/2017 refatored : Jose Retamal
 */
namespace MTProjectPegSolitaire
{
    public class TimeKeeper : StackPanel
    {
        #region class variables
        //class variables
        private int TimerFontSize { get; set; }
        private int seconds { get; set; }
        private int minutes { get; set; }

        private DispatcherTimer timer { get; set; }

        //gui variables
        static TextBlock minutesTB;
        static TextBlock colonTB;
        static TextBlock secondsTB;

        #endregion
        #region constructors
        //constructor with starting time as parameter
        public TimeKeeper(int TimerSize)
        {
            TimerFontSize = TimerSize;
            //create disptchertimer and set interval to 1 second
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            //add tick event
            timer.Tick += timer_Tick;

            CreateTimerGUI();

            //intitialaze minutes and seconds
            seconds = 0;
            minutes = 0;

        }
        #endregion
        #region create GUI and time
        private void CreateTimerGUI()
        {
            //set stack panel
            //one for background
            StackPanel BackSP = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                BorderBrush = new SolidColorBrush(Colors.DarkGray),
                CornerRadius = new CornerRadius(8, 8, 8, 8),
                BorderThickness = new Thickness(4),
                Background = new SolidColorBrush(Colors.White),
                Orientation = Orientation.Horizontal

            };
            this.Orientation = Orientation.Horizontal;
            this.VerticalAlignment = VerticalAlignment.Center;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.BorderBrush = new SolidColorBrush(Colors.Gray);
            this.BorderThickness = new Thickness(3);
            this.CornerRadius = new CornerRadius(8, 8, 8, 8);
            this.Background = new SolidColorBrush(Colors.Gray);

            //GUI
            minutesTB = new TextBlock()
            {
                Foreground = new SolidColorBrush(Colors.Gray),
                FontSize = TimerFontSize,
                Text = "00"

            };
            colonTB = new TextBlock()
            {
                Foreground = new SolidColorBrush(Colors.Gray),
                FontSize = TimerFontSize,
                Text = ":"

            };
            secondsTB = new TextBlock()
            {
                Foreground = new SolidColorBrush(Colors.Gray),
                FontSize = TimerFontSize,
                Text = "00"

            };
            BackSP.Children.Add(minutesTB);
            BackSP.Children.Add(colonTB);
            BackSP.Children.Add(secondsTB);
            this.Children.Add(BackSP);
        }
        //control the time will tick every one second
        private void timer_Tick(object sender, object e)
        {
            //increse second        
            seconds++;
            //add a minut when second reach 59
            if (seconds == 59)
            {
                seconds = 0;
                minutes++;
            }

            //put 0 before if second is less tahn 10
            if (seconds < 10)
            {
                secondsTB.Text = "0" + seconds.ToString();
            }
            else
            {
                secondsTB.Text = seconds.ToString();
            }

            if (minutes < 10)
            {
                minutesTB.Text = "0" + minutes.ToString();
            }
            else
            {
                minutesTB.Text = minutes.ToString();
            }
        }
        #endregion
        #region time control 

        public void StartTimer()
        {
            timer.Start();

        }
        public void StopTimer()
        {
            timer.Stop();
        }
        public void setTime(int second)
        {
            int minutes = (int)(second / 60);


            this.minutes = minutes;
            this.seconds = second - minutes * 60;
        }
        #endregion
        #region public return methods
        public int GetTotalSeconds()
        {
            int seccondsToReturn = 0;
            seccondsToReturn += seconds;
            seccondsToReturn += minutes * 60;
            return seccondsToReturn;
        }
        public String GetTime()
        {
            return "" + minutes + ":" + seconds;
        }

        #endregion
    }
}
