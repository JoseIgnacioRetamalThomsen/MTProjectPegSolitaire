using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MTProjectPegSolitaire
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOverPage : Page
    {
        public GameOverPage()
        {

            this.InitializeComponent();

            ScoreTB.Text += App.lastScore;




            if (App.lastPiecesLeft == 1)
            {
                ResultTB.Text = "Success";
            }
            else
            {
                ResultTB.Text = "Fail";
            }

            PiecesRemovedTB.Text = "" + App.lastPiecesRemoved;
            TimeTB.Text = App.lastTotalTime;

            //save last score to app seting
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            try
            {
                localSettings.Values["LastTimeInSeconds"] = App.lastTotalTimeSecond;
                localSettings.Values["LastPiecesRemoved"] = App.lastPiecesRemoved;
            }
            catch (Exception exp) { }

            //high scores
            int highScore3 = 0, highScore2 = 0, highScore1 = 0;
            String highScore3Name = "", highScore2Name = "", highScore1Name = "";
            #region get high score from storage or create if not exist
            try
            {
                highScore3 = Convert.ToInt32((localSettings.Values["HighScore3"]).ToString());
                highScore3Name = (localSettings.Values["HighScore3Name"]).ToString();
                highScore2 = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                highScore2Name = (localSettings.Values["HighScore2Name"]).ToString();
                highScore1 = Convert.ToInt32((localSettings.Values["HighScore1"]).ToString());
                highScore1Name = (localSettings.Values["HighScore1Name"]).ToString();
            }
            catch (Exception exc3)
            {
                //if do not existe create
                localSettings.Values["HighScore3"] = 0;
                localSettings.Values["HighScore3Name"] = "";
                try
                {
                    highScore2 = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                    highScore2Name = (localSettings.Values["HighScore2Name"]).ToString();
                }
                catch (Exception exc2)
                {
                    localSettings.Values["HighScore2"] = 0;
                    localSettings.Values["HighScore2Name"] = "";

                    try
                    {
                        highScore1 = Convert.ToInt32((localSettings.Values["HighScore1"]).ToString());
                        highScore1Name = (localSettings.Values["HighScore1Name"]).ToString();
                    }
                    catch (Exception exc1)
                    {
                        localSettings.Values["HighScore1"] = 0;
                        localSettings.Values["HighScore1Name"] = "";
                    }

                }
            }
            #endregion

            Debug.WriteLine(Convert.ToInt32((localSettings.Values["HighScore3"]).ToString()) + " " + Convert.ToInt32((localSettings.Values["HighScore2"]).ToString()) + " " + Convert.ToInt32((localSettings.Values["HighScore1"]).ToString()));

            bool haveHighScore = false;
            //check and assig if any high score
            if (App.lastScore > highScore1)
            {
                highScore1 = App.lastScore;
                highScore2 = Convert.ToInt32((localSettings.Values["HighScore1"]).ToString());
                highScore3 = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                localSettings.Values["HighScore1"] = highScore1;
                localSettings.Values["HighScore2"] = highScore2;
                localSettings.Values["HighScore3"] = highScore3;

            }
            else if (App.lastScore > highScore2)
            {
                highScore2 = App.lastScore;
                highScore3 = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                localSettings.Values["HighScore2"] = highScore2;
                localSettings.Values["HighScore3"] = highScore3;

            }
            else if (App.lastScore > highScore3)
            {
                highScore3 = App.lastScore;
                localSettings.Values["HighScore3"] = highScore3;

            }
            HighScore1Score.Text = highScore1.ToString();
            HighScore2Score.Text = highScore2.ToString();
            HighScore3Score.Text = highScore3.ToString();

        }
    }
}
