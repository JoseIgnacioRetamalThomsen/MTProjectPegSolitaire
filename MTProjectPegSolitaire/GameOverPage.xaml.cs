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

        int haveHighScore;
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
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
        
            try
            {
                localSettings.Values["LastTimeInSeconds"] = App.lastTotalTimeSecond;
                localSettings.Values["LastPiecesRemoved"] = App.lastPiecesRemoved;
            }
            catch (Exception exp) { }


            

                    

            //0 if not high score, 1 high1 ...
             haveHighScore = 0;
            //check and assig if any high score
            if (App.lastScore > App.highScores[0])
            {
                //update high scores and names
                App.highScores[0] = App.lastScore;
                try
                {
                    App.highScores[1] = Convert.ToInt32((localSettings.Values["HighScore1"]).ToString());
                    App.highScores[2] = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                }catch(Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
                localSettings.Values["HighScore1"] = App.highScores[0];
                localSettings.Values["HighScore2"] = App.highScores[1];
                localSettings.Values["HighScore3"] = App.highScores[2];

                //HighScore1Name.Text = "1ST " + "Unknown";
                localSettings.Values["HighScore1Name"] = "Unknown";
                App.highScoresName[0] = "Unknown";

                haveHighScore = 1;

            }
            else if (App.lastScore > App.highScores[1])
            {
                //update high scores
                App.highScores[1] = App.lastScore;
                Debug.WriteLine(localSettings.Values["HighScore2"]);
                App.highScores[2] = Convert.ToInt32((localSettings.Values["HighScore2"]).ToString());
                localSettings.Values["HighScore2"] = App.highScores[1];
                localSettings.Values["HighScore3"] = App.highScores[2];

               // HighScore2Name.Text = "2ND " + "Unknown";
                localSettings.Values["HighScore2Name"] = "Unknown";
                App.highScoresName[1] = "Unknown";

                haveHighScore = 2;

            }
            else if (App.lastScore > App.highScores[2])
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

            HighScore1Name.Text ="1ST "+ App.highScoresName[0];
            HighScore2Name.Text = "2ND  "+App.highScoresName[1];
            HighScore3Name.Text = "3RD  "+App.highScoresName[2];
            if (haveHighScore > 0)
            {
                HavaHighScoreSP.Visibility = Visibility.Visible;
            }
        }

        private void highScoreNameButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            String localHighScoreName = highScoreNameInput.Text;

            if (haveHighScore > 0)
            {
               
                switch (haveHighScore)
                {
                    case 1:
                        
                        HighScore1Name.Text = "1ST " + localHighScoreName;
                        localSettings.Values["HighScore1Name"] = localHighScoreName;
                        break;
                    case 2:
                        
                        HighScore2Name.Text = "2ST " + localHighScoreName;
                        localSettings.Values["HighScore2Name"] = localHighScoreName;
                        break;
                    case 3:
                       
                        HighScore3Name.Text = "3RD " + localHighScoreName;
                        localSettings.Values["HighScore3Name"] = localHighScoreName;
                        break;

                }
            }
        }
    }
}
