using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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
            ImageBrush BoardBackground = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\BlackWoodBackground.jpg")) };
            ImageBrush HoleBackground = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\lightBack.jpg")) };
            ImageBrush PieceBackgrounImage = new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\greenSphere.jpg")) };

            Board boardGame = new Board(5, BoardBackground, HoleBackground, PieceBackgrounImage);

            GamePageMainSP.Children.Add(boardGame);
            boardGame.PlacePieces();
            boardGame.RemovePiece(1, 1);
        }
    }
}
