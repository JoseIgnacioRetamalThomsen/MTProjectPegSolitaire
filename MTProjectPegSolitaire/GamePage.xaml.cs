using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


/*
 *   Jose Ignacio Retamal Peg Solitaire Game -
 *   This page 
 *   17/11/2017 File created. Jose Ignacio Retamal.
 *   17/11/2017 Page create and add a Board object wich is the game,
 *              Set image URI, they are parameters for the board object. Jose Ignacio Retamal.
 *   19/11/2017 Timer Object added. Jose Ignacio Retamal.
 */

namespace MTProjectPegSolitaire
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
       


        public GamePage()
        {
            this.InitializeComponent();
          
            //StartTimer();
            // ShowGameOver();
            App.timer = new TimeKeeper(40);
            TimerSP.Children.Add(App.timer);
            


            //create board
            ImageBrush BoardBackground = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\Wood_1.jpg")) };
            BoardBackground.Stretch = Stretch.UniformToFill;
            ImageBrush HoleBackground = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\lightBack.jpg")) };
            ImageBrush PieceBackgrounImage = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\greenSphere.jpg")) };

            if (App.continueGame)
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                String oneArrayBoard = localSettings.Values["boardArray"].ToString();
                int piecesRmoves = Convert.ToInt32(localSettings.Values["LastPiecesRemoves"]) -1;
                int timeToContinue = Convert.ToInt32(localSettings.Values["LastTime"]);
                App.board = new Board(oneArrayBoard, piecesRmoves, BoardBackground, HoleBackground, PieceBackgrounImage, this);
                GamePageMainSP.Children.Add(App.board);
                App.board.placePieceFromArray();
                App.timer.setTime(timeToContinue);
                App.timer.StartTimer();
            }
            else
            {
                App.board = new Board(App.lastBoardSize, BoardBackground, HoleBackground, PieceBackgrounImage, this);



                GamePageMainSP.Children.Add(App.board);
                App.board.PlacePieces();
                App.board.RemoveRandonPiece();
                App.timer.StartTimer();
            }
            App.board.setBoardArrayWithOneString(App.board.getBoardArrayInOneString());
        }

        private void Test_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine(App.timer.GetTotalSeconds());
        }

        public async Task GameOverAsync()
        {

            //show game over
            GameOverTF.Visibility = Visibility.Visible;

            App.timer.StopTimer();
            App.lastTotalTimeSecond = App.timer.GetTotalSeconds();
            App.lastPiecesRemoved = App.board.GetPieceRemoved();
            App.lastTotalTime = App.timer.GetTime();
            App.lastPiecesLeft = Board.getPiecesLeft(App.lastBoardSize, App.lastPiecesRemoved);

            Debug.WriteLine("l"+App.lastPiecesLeft);

            App.lastScore = Board.getScore(App.timer.GetTotalSeconds(), App.lastBoardSize, App.lastPiecesRemoved);

            await Task.Delay(1500);

            this.Frame.Navigate(typeof(GameOverPage), null);

            //remove saved game
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["boardArray"] = "0";
        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //save board to local storage
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["boardArray"] = App.board.getBoardArrayInOneString();
            
            localSettings.Values["LastTime"] = App.timer.GetTotalSeconds();
            localSettings.Values["LastPiecesRemoves"] = App.board.GetPieceRemoved();

            this.Frame.Navigate(typeof(MainPage), null);

        }


    }
}
