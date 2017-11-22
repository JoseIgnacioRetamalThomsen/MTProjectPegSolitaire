using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MTProjectPegSolitaire
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int boardSize = 5;
        //array taht represent the board with pieces
        Boolean[][] boardArray;
        public MainPage()

        {
            this.InitializeComponent();

<<<<<<< HEAD
            this.Loading += MainPage_Loading;



        }

        private void MainPage_Loading(FrameworkElement sender, object args)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            try
            {
                //get last board size
                App.lastBoardSize = Convert.ToInt32((localSettings.Values["BoardSize"]).ToString());
            }
            catch (Exception exc)
            {
                //this will be if is first time runig the app, will create baord size
                localSettings.Values["BoardSize"] = App.lastBoardSize;
                Debug.WriteLine(exc.StackTrace);

            }
            //set radio button to checked
            if (App.lastBoardSize == 5)
            {
                EasyRB.IsChecked = true;
            }
            else if (App.lastBoardSize == 7)
            {
                MediunRB.IsChecked = true;
            }
            else if (App.lastBoardSize == 9)
            {
                HardRB.IsChecked = true;
            }
        }






=======
>>>>>>> 396c6f2ad0f280e6cc4d037ca9d0c101043f42f6




        }

        //move have to be done here

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MediaElement sound = new MediaElement()
            {
                AudioDeviceType = AudioDeviceType.Multimedia,
                Source = new Uri(this.BaseUri, @"Assets\images\bells001.mp3"),
            };
            sound.Play();
            sound.AutoPlay = true;
        }


        private void Button_NewGame_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage), null);
        }

        private void Button_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            MediaElement sound = new MediaElement()
            {
                AudioDeviceType = AudioDeviceType.Multimedia,
                Source = new Uri(this.BaseUri, @"Assets\images\bells001.mp3"),
            };
            sound.Play();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //set board size and naviete to game , mean start the game
            if (EasyRB.IsChecked == true)
            {
                App.lastBoardSize = 5;

            }
            else if (MediunRB.IsChecked == true)
            {
                App.lastBoardSize = 7;
            }
            else if (HardRB.IsChecked == true)
            {
                App.lastBoardSize = 9;
            }

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            try
            {
                localSettings.Values["BoardSize"] = App.lastBoardSize;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.StackTrace);
            }
        }
    }
}
