using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MTProjectPegSolitaire
{
    class TimeKeeper : StackPanel
    {
        //variables
        private int TimerFontSize { get; set; }
        private int seconds { get; set; }
        private int minutes { get; set; }

        DispatcherTimer timer;

        TextBlock minutesTB;
        TextBlock colonTB;
        TextBlock secondsTB;




        //constructor
        public TimeKeeper(int TimerSize)
        {
            TimerFontSize = TimerSize;
            //create disptchertimer and set interval to 1 second
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            //add tick event
            timer.Tick += timer_Tick;

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

            //intitialaze minutes and seconds
            seconds = 0;
            minutes = 0;

        }

        public void StartTimer()
        {
            timer.Start();

        }
        public void StopTimer()
        {
            timer.Stop();
        }
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
    }
}
