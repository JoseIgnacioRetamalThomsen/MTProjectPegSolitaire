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

/*
 * Jose Ignacio Retamal Triangle peg solitaraire game
 * Page that will show after the game where is display the score, time,pegs removes and high score table
 * 
 */

namespace MTProjectPegSolitaire
{
    /// <summary>
    /// game over page
    /// </summary>
    public sealed partial class GameOverPage : Page
    {
        #region class variables
        //variable for check if have high score, 0 no high score , 1 first have score ,2 secon...
        private int haveHighScore { get; set; }

        #endregion
        #region constructors
        //constructor no parameter
        public GameOverPage()
        {

            this.InitializeComponent();
            this.Loading += GameOver_Loading;
            this.Loading += SetScore_Loading;


        }
        #endregion
        #region Loading methods
        private void SetScore_Loading(FrameworkElement sender, object args)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            //set score 
            ScoreTB.Text += App.lastScore;

            if (App.lastPiecesLeft == 1)
            {
                ResultTB.Text = "Success";
            }
            else
            {
                ResultTB.Text = "Fail";
            }

            PiecesRemovedTB.Text = "" + (App.lastPiecesRemoved - 1);
            TimeTB.Text = App.lastTotalTime;

            //save last score to app seting
            try
            {
                localSettings.Values["LastTimeInSeconds"] = App.lastTotalTimeSecond;
                localSettings.Values["LastPiecesRemoved"] = App.lastPiecesRemoved;
            }
            catch { }

            //0 if not high score, 1 high1, 2 high2 ...
            haveHighScore = 0;
            //check and assig if any high score
            //first score
            if (App.lastScore > App.highScores[0])
            {
                //update high scores and names
                App.highScores[0] = App.lastScore;
                try
                {
                    //one score down
                    App.highScores[1] = Convert.ToInt32((localSettings.Values["HighScore1"]).ToString());
                    App.highScores[2] = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                }
                catch
                {

                }
                localSettings.Values["HighScore1"] = App.highScores[0];
                localSettings.Values["HighScore2"] = App.highScores[1];
                localSettings.Values["HighScore3"] = App.highScores[2];

                //HighScore1Name.Text = "1ST " + "Unknown";
                localSettings.Values["HighScore1Name"] = "Unknown";
                App.highScoresName[0] = "Unknown";

                haveHighScore = 1;

            }
            else if (App.lastScore > App.highScores[1])//second score
            {
                //update high scores
                App.highScores[1] = App.lastScore;
                //Debug.WriteLine(localSettings.Values["HighScore2"]);
                App.highScores[2] = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                localSettings.Values["HighScore2"] = App.highScores[1];
                localSettings.Values["HighScore3"] = App.highScores[2];

                // HighScore2Name.Text = "2ND " + "Unknown";
                localSettings.Values["HighScore2Name"] = "Unknown";
                App.highScoresName[1] = "Unknown";

                haveHighScore = 2;

            }
            else if (App.lastScore > App.highScores[2])//third high score
            {
                App.highScores[2] = App.lastScore;
                Debug.WriteLine(localSettings.Values["HighScore3"]);
                localSettings.Values["HighScore3"] = App.highScores[2];

                // HighScore3Name.Text = "3RD " + "Unknown";
                localSettings.Values["HighScore3Name"] = "Unknown";
                App.highScoresName[2] = "Unknown";

                haveHighScore = 3;

            }
            //set value in gui
            HighScore1Score.Text = App.highScores[0].ToString();
            HighScore2Score.Text = App.highScores[1].ToString();
            HighScore3Score.Text = App.highScores[2].ToString();

            HighScore1Name.Text = "1ST " + App.highScoresName[0];
            HighScore2Name.Text = "2ND  " + App.highScoresName[1];
            HighScore3Name.Text = "3RD  " + App.highScoresName[2];

            if (haveHighScore > 0)
            {
                HavaHighScoreSP.Visibility = Visibility.Visible;
                //sound
                if (App.isSound) App.aplauseHigh.Play();

            }
            else
            {   //sound
                if (App.isSound) App.aplauseNor.Play();
            }

        }

        private void GameOver_Loading(FrameworkElement sender, object args)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["boardArray"] = "0";
            App.isOnGameOverPage = true;
        }
        #endregion
        #region button listeners
        private void highScoreNameButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            String localHighScoreName = highScoreNameInput.Text;

            if (haveHighScore > 0)
            {

                switch (haveHighScore)
                {
                    case 1:

                        HighScore1Name.Text = "1ST " + localHighScoreName;
                        localSettings.Values["HighScore1Name"] = localHighScoreName;
                        App.highScoresName[0] = localHighScoreName;
                        break;
                    case 2:

                        HighScore2Name.Text = "2ST " + localHighScoreName;
                        localSettings.Values["HighScore2Name"] = localHighScoreName;
                        App.highScoresName[1] = localHighScoreName;
                        break;
                    case 3:

                        HighScore3Name.Text = "3RD " + localHighScoreName;
                        localSettings.Values["HighScore3Name"] = localHighScoreName;
                        App.highScoresName[2] = localHighScoreName;
                        break;

                }
            }
        }


        private void HOME_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //sound
            if (App.isSound) App.tappedSound.Play();

                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            //reset game over for not able to continue a game when is over
            //is for fix a bug taht i cant find...
            localSettings.Values["boardArray"] = "0";
            App.isOnGameOverPage = true;

            this.Frame.Navigate(typeof(MainPage), null);
        }
        #endregion
    }
}
