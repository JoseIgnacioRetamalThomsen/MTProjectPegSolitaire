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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;




namespace MTProjectPegSolitaire
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {

        TimeKeeper timer;
        Board board;
        public GamePage()
        {
            this.InitializeComponent();

            //StartTimer();
            // ShowGameOver();
            timer = new TimeKeeper(40);
            TimerSP.Children.Add(timer);
            timer.StartTimer();

            //create board
            ImageBrush BoardBackground = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\images\back.jpg")) };
            ImageBrush HoleBackground = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\images\lightBack.jpg")) };
            ImageBrush PieceBackgrounImage = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\images\greenSphere.jpg")) };

            board = new Board(5, BoardBackground, HoleBackground, PieceBackgrounImage, this);

            GamePageMainSP.Children.Add(board);
            board.PlacePieces();
            board.RemovePiece(1, 1);

        }


        private void Test_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine(timer.GetTotalSeconds());
        }

       




        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);

        }
    }
}
