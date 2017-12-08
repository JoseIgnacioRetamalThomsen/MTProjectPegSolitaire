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


/*
 *   Jose Ignacio Retamal Peg Solitaire Game - Landing game page.
 *   17/11/2017 File created: Jose Ignacio Retamal.
 *   20/11/2017 Static GUI added to xamal file : Jose Ignacio Retamal.
 */

namespace MTProjectPegSolitaire
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       // int boardSize = 5;
        //array that represent the board with pieces
       // Boolean[][] boardArray;
        public MainPage()

        {
            this.InitializeComponent();

            //add loading event
            this.Loading += LoadBoardSize_Loading;
            this.Loading += LoadScores_Loding;
            this.Loading += LoadGame_Loading;
        }

        private void LoadGame_Loading(FrameworkElement sender, object args)
        {
            //left game over page
            App.isOnGameOverPage = false;

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            String oneArrayBoard = "";
            try
            {
                oneArrayBoard = localSettings.Values["boardArray"].ToString();
            }catch(Exception ex)
            {
                localSettings.Values["boardArray"] = "0";
                oneArrayBoard = "0";
            }
            if(oneArrayBoard.Substring(0,1)!="0")
            {
                ContinueButton.Visibility = Visibility.Visible;
            }
        }

        private void LoadScores_Loding(FrameworkElement sender, object args)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            try

            {
               

                //if high score 1 do not exist mean that non other
                App.highScores[0] = Convert.ToInt32((localSettings.Values["HighScore1"]).ToString());
                App.highScoresName[0] = localSettings.Values["HighScore1Name"].ToString();
                //if highScore1 exits all other are at least ini to 0
                App.highScores[1] = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                App.highScoresName[1] = localSettings.Values["HighScore2Name"].ToString();

                App.highScores[2] = Convert.ToInt32((localSettings.Values["HighScore3"]).ToString());
                App.highScoresName[2] = localSettings.Values["HighScore3Name"].ToString();

            }
            catch (Exception exception)
            {
               // Debug.WriteLine(exception);
                localSettings.Values["HighScore3"] = 0;
                localSettings.Values["HighScore2"] = 0;
                localSettings.Values["HighScore1"] = 0;

                localSettings.Values["HighScore1Name"] = "Non One";
                localSettings.Values["HighScore2Name"] = "Non One";
                localSettings.Values["HighScore3Name"] = "Non One";
                
            }
        }

        private void LoadBoardSize_Loading(FrameworkElement sender, object args)
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


        //move have to be done here

            
        private void Button_NewGame_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //flas for new game
            App.continueGame = false;
            this.Frame.Navigate(typeof(GamePage), null);
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

        private void ContinueButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.continueGame = true;
            this.Frame.Navigate(typeof(GamePage), null);
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void instructiosRB_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
