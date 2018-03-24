﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

/*
 * Jose Ignacio Retamal 2017 - main game calss
 *  This class create the boad an set all the game, need to be place in a container
 *   17/11/2017 File Created : first methods started
 *   06/12/2017 method for save boardArray in local storage, get boardArray in one string and set board array with 1 string
 *   10/12/2017: sound :Jose Retamal ,
 */

namespace MTProjectPegSolitaire
{
    /// <summary>
    /// Main game object
    /// </summary>
    public class Board : Grid
    {
        #region class variables
        int scale;
        int stroke;
        int boardSize;
        StackPanel BackSP;
        Point pieceToMove;

        GamePage gamePage;

        ImageBrush BoardBackGroundImage, PieceHolderBackgroundImage, PieceBackgroundImage;

        Boolean[][] boardArray;
        int piecesRemoved;
        //sound
        MediaPlayer pegTapped = new MediaPlayer();
        MediaPlayer pegJumped = new MediaPlayer();
        MediaPlayer brongTap = new MediaPlayer();

        #endregion
        #region constructors
        //new game
        public Board(int size, ImageBrush dackGroundImage, ImageBrush lightBack, ImageBrush PieceBackgroundImage, GamePage gamePage, MediaPlayer pegTapped,MediaPlayer pegJumped,MediaPlayer brongTap)
        {

            this.boardSize = size;
            BoardBackGroundImage = dackGroundImage;
            PieceHolderBackgroundImage = lightBack;
            this.PieceBackgroundImage = PieceBackgroundImage;

            piecesRemoved = 0;
            CreateBoard();
            createBoardArray();


            this.gamePage = gamePage;
            //sound
            this.pegTapped = pegTapped;
            this.brongTap = brongTap;
            this.pegJumped = pegJumped;
        }
        public Board(String oneStringArray,int piecesRemove, ImageBrush dackGroundImage, ImageBrush lightBack, ImageBrush PieceBackgroundImage, GamePage gamePage, MediaPlayer pegTapped, MediaPlayer pegJumped, MediaPlayer brongTap)
        {
            this.setBoardArrayWithOneString(oneStringArray);//creates board array
            BoardBackGroundImage = dackGroundImage;
            PieceHolderBackgroundImage = lightBack;
            this.PieceBackgroundImage = PieceBackgroundImage;

            this.piecesRemoved = piecesRemove;
            CreateBoard();
            this.gamePage = gamePage;

            //sound
            this.pegTapped = pegTapped;
            this.brongTap = brongTap;
            this.pegJumped = pegJumped;

        }
        #endregion
        #region create baord and place piece methods
        private void CreateBoard()
        {
            //calculate scale for diferent amount of pieces , we have for 3 size 5,7,9, and ssig stroke
            stroke = 3;
            if (boardSize == 7)
            {
                scale = 70;
            }
            else if (boardSize == 9)
            {
                scale = 55;
                stroke = 2;
            }
            else
            {
                scale = 100;
            }

            //set size of grid(this)
            this.Width = 600;
            //3d effect
            
            PlaneProjection planeProjection = new PlaneProjection();
            planeProjection.RotationX = -15;
            this.Projection = planeProjection;
            
            //create board:
            //1.- Create back stack panel
            BackSP = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Name = "BoarBackdSP"
            };

            //2.-create background poligon
            var triangle = new Polygon();
            //set background to polygon from image
            triangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);//BoardBackGroundImage;


            var points = new PointCollection();
            points.Add(new Windows.Foundation.Point(0, 510));
            points.Add(new Windows.Foundation.Point(0, 500));
            points.Add(new Windows.Foundation.Point(295, -40));
            points.Add(new Windows.Foundation.Point(305, -40));
            points.Add(new Windows.Foundation.Point(600, 500));
            points.Add(new Windows.Foundation.Point(600, 510));
            //  points.Add(new Windows.Foundation.Point(180, 200));
            triangle.Points = points;

            //3.- ADd poligon and stack panel for pieces to grid(this) to 0,0 in grid
            this.Children.Add(triangle);
            this.Children.Add(BackSP);

            //create grid for each row of pieces
            for (int row = 0; row < boardSize; row++)
            {
                //crete transparent grid for background an put pieces
                Grid backGrid = new Grid()
                {
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Background = new SolidColorBrush(Colors.Transparent),
                    Name = "BackGrid_" + row,

                };
                backGrid.RowDefinitions.Add(new RowDefinition());
                //create  colums in grid
                for (int col = 0; col < (row + 1); col++)
                {
                    backGrid.ColumnDefinitions.Add(new ColumnDefinition());

                }

                //add componets
                for (int col = 0; col < (row + 1); col++)
                {
                    //add background
                    Border backgroundBorder = new Border()
                    {
                        Width = ((100 * scale) / 100),
                        Height = ((100 * scale) / 100),
                        Background = new SolidColorBrush(Colors.Transparent)
                    };
                    backgroundBorder.SetValue(Grid.ColumnProperty, col);
                    backgroundBorder.SetValue(Grid.RowProperty, 0);

                    //add ellipse for show when focused
                    Ellipse FocusCircle = new Ellipse()
                    {
                        Fill = new SolidColorBrush(Colors.Gold),
                        Width = ((90 * scale) / 100),
                        Height = ((90 * scale) / 100),
                        Name = "FocusCircle_" + row + "_" + col,
                    };
                    FocusCircle.SetValue(Grid.ColumnProperty, col);
                    FocusCircle.SetValue(Grid.RowProperty, 0);

                    FocusCircle.Visibility = Visibility.Collapsed;

                    //create ellipse for piece holder
                    Ellipse PieceHolder = new Ellipse()
                    {
                        Fill = PieceHolderBackgroundImage,
                        Width = ((80 * scale) / 100),
                        Height = ((80 * scale) / 100),
                        StrokeThickness = stroke,//2,//3,
                        Stroke = new SolidColorBrush(Colors.Black),
                        Name = "PieceHolder" + row + "_" + col,


                    };
                    //set position in grid
                    PieceHolder.SetValue(Grid.ColumnProperty, col);
                    PieceHolder.SetValue(Grid.RowProperty, 0);

                    //add to grid
                    backGrid.Children.Add(backgroundBorder);
                    backGrid.Children.Add(FocusCircle);
                    backGrid.Children.Add(PieceHolder);
                }

                StackPanel s = new StackPanel();
                // s.Children.Add(backGrid);



                BackSP.Children.Add(backGrid);
            }//for i create board
        }
        private void createBoardArray()
        {
            boardArray = new Boolean[boardSize][];
            for (int i = 0; i < boardSize; i++)
            {
                boardArray[i] = new bool[i + 1];
            }
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < (i + 1); j++)
                    boardArray[i][j] = true;
            }
        }
        public String getBoardArrayInOneString()
        {
            StringBuilder arrayInString = new StringBuilder("");
            //first number is going to be the size of the table 3,5 or 7
            arrayInString.Append(boardSize);
            //1 for tru 0 for false
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < (i + 1); j++)
                {
                    if (boardArray[i][j])
                        arrayInString.Append("1");
                    else
                        arrayInString.Append("0");
                }
            }
            // Debug.WriteLine(arrayInString);
            return arrayInString.ToString();
        }
        public void setBoardArrayWithOneString(String boardArrayString)
        {
            this.boardSize = Convert.ToInt32(boardArrayString.Substring(0, 1));
            this.createBoardArray();
            Debug.WriteLine(boardSize);
            int postionInString = 1;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < (i + 1); j++)
                {
                    if (Convert.ToInt32(boardArrayString.Substring(postionInString++, 1)) == 1)
                        boardArray[i][j] = true;
                    else
                        boardArray[i][j] = false;
                }
            }
        }
        public void PlacePieces()
        {
            int scale = 1;
            if (boardSize == 7)
            {
                scale = 70;
            }
            else if (boardSize == 9)
            {
                scale = 55;
            }
            else
            {
                scale = 100;
            }
            for (int i = 0; i < boardSize; i++)
            {
                //get grid
                String GridName = "BackGrid_" + i;
                Grid BackGrid = (Grid)FindName(GridName);

                for (int j = 0; j < (i + 1); j++)
                {
                    //create piece
                    Ellipse Piece = new Ellipse()
                    {
                        Fill = PieceBackgroundImage, //new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\images\ESF.jpg")) }, //new SolidColorBrush(Colors.OrangeRed),
                        Width = ((70 * scale) / 100),
                        Height = ((70 * scale) / 100),
                        StrokeThickness = 2,
                        Stroke = new SolidColorBrush(Colors.Silver),
                        Name = "Piece" + i + "_" + j,


                    };
                    //set position in grid
                    Piece.SetValue(Grid.ColumnProperty, j);
                    Piece.SetValue(Grid.RowProperty, 0);

                    //add action listener
                    Piece.Tapped += PieceTappedFirstTime;

                    //add piece to border
                    BackGrid.Children.Add(Piece);
                }
            }
        }//PlacePiece
        public void placePieceFromArray()
        {
            int scale = 1;
            if (boardSize == 7)
            {
                scale = 70;
            }
            else if (boardSize == 9)
            {
                scale = 55;
            }
            else
            {
                scale = 100;
            }
            for (int i = 0; i < boardSize; i++)
            {
                //get grid
                String GridName = "BackGrid_" + i;
                Grid BackGrid = (Grid)FindName(GridName);

                for (int j = 0; j < (i + 1); j++)
                {
                    if (boardArray[i][j] == true)
                    {
                        //create piece
                        Ellipse Piece = new Ellipse()
                        {
                            Fill = PieceBackgroundImage, //new ImageBrush() { ImageSource = new BitmapImage(new Uri(this.BaseUri, @"Assets\images\ESF.jpg")) }, //new SolidColorBrush(Colors.OrangeRed),
                            Width = ((70 * scale) / 100),
                            Height = ((70 * scale) / 100),
                            StrokeThickness = 2,
                            Stroke = new SolidColorBrush(Colors.Silver),
                            Name = "Piece" + i + "_" + j,


                        };
                        //set position in grid
                        Piece.SetValue(Grid.ColumnProperty, j);
                        Piece.SetValue(Grid.RowProperty, 0);

                        //add action listener
                        Piece.Tapped += PieceTappedFirstTime;

                        //add piece to border
                        BackGrid.Children.Add(Piece);
                    }
                }
            }
        }//place pieces from array
        #endregion
        #region manipulating pieces methods

        public void RemoveRandonPiece()
        {
            int row, col;
            row = App.random.Next(0, boardSize);
            col = App.random.Next(0, row + 1);
            RemovePiece(row, col);
        }


        public void RemovePiece(int i, int j)
        {
            //get grid for remove piece
            String GridName = "BackGrid_" + i;
            Grid BackGrid = (Grid)FindName(GridName);
            //get piece to remove
            String PieceName = "Piece" + i + "_" + j;
            Ellipse PieceToRemove = (Ellipse)FindName(PieceName);
            //remove piece from board
            BackGrid.Children.Remove(PieceToRemove);
            //remove piece in array
            boardArray[i][j] = false;

        }
        private void AddPiece(int i, int j)
        {
            int scale = 1;
            if (boardSize == 7)
            {
                scale = 70;
            }
            else if (boardSize == 9)
            {
                scale = 55;
            }
            else
            {
                scale = 100;
            }
            String GridName = "BackGrid_" + i;
            Grid BackGrid = (Grid)FindName(GridName);

            Ellipse Piece = new Ellipse()
            {
                Fill = PieceBackgroundImage,//new SolidColorBrush(Colors.OrangeRed),
                Width = ((70 * scale) / 100),
                Height = ((70 * scale) / 100),
                StrokeThickness = 2,
                Stroke = new SolidColorBrush(Colors.Silver),
                Name = "Piece" + i + "_" + j,


            };

            Piece.SetValue(Grid.ColumnProperty, j);
            Piece.SetValue(Grid.RowProperty, 0);
            Piece.Tapped += PieceTappedFirstTime;

            boardArray[i][j] = true;

            BackGrid.Children.Add(Piece);
        }
        #endregion
        #region checking methods (returns)
        public int GetPieceRemoved()
        {
            return this.piecesRemoved + 1;
        }

        private bool CheckIfCanMove(int i, int j, Point[] moves)
        {
            //  return parameter
            // moves = new Point[4];
            int PosibleI, PosibleJ;
            //piece to jump position
            int IPieceToJump, JPieceToJump;


            bool canMove = false;
            //there are 6 posible moves

            //move 1  ((i-2),0)
            PosibleI = i - 2;
            PosibleJ = j;

            //boardArray[PosibleI][PosibleJ] may no existe
            try
            {
                IPieceToJump = Math.Abs((i + PosibleI) / 2);
                JPieceToJump = Math.Abs((j + PosibleJ) / 2);
                if (PosibleI >= 0 && PosibleJ <= PosibleI)
                {
                    if (boardArray[PosibleI][PosibleJ] == false && boardArray[IPieceToJump][JPieceToJump] == true)
                    {
                        //move is posible
                        moves[0] = new Point(PosibleI, PosibleJ);
                        canMove = true;
                                            }
                    else
                    {
                        moves[0] = new Point(99, 99);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.WriteLine("can't move" + PosibleI + " " + PosibleJ + "trow in move 1" +e.StackTrace);
                moves[0] = new Point(99, 99);
            }

            //move 2 ((i+2),0)
            PosibleI = i + 2;
            PosibleJ = j;

            try
            {
                IPieceToJump = Math.Abs((i + PosibleI) / 2);
                JPieceToJump = Math.Abs((j + PosibleJ) / 2);
                if (PosibleI <= (boardSize - 1) && PosibleJ <= PosibleI)
                {
                    if (boardArray[PosibleI][PosibleJ] == false && boardArray[IPieceToJump][JPieceToJump] == true)
                    {
                        //move is posible
                        moves[1] = new Point(PosibleI, PosibleJ);
                        canMove = true;
                        // Debug.WriteLine("can move");
                    }
                    else
                    {
                        moves[1] = new Point(99, 99);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.WriteLine("can't move" + PosibleI + " " + PosibleJ + "trow in move 2" +e.StackTrace);
            }

            //move 3 (0,(j+2))
            PosibleI = i;
            PosibleJ = j + 2;
            try
            {
                IPieceToJump = Math.Abs((i + PosibleI) / 2);
                JPieceToJump = Math.Abs((j + PosibleJ) / 2);
                if (PosibleJ <= PosibleI)
                {
                    if (boardArray[PosibleI][PosibleJ] == false && boardArray[IPieceToJump][JPieceToJump] == true)
                    {
                        //move is posible
                        moves[2] = new Point(PosibleI, PosibleJ);
                        canMove = true;
                        // Debug.WriteLine("can move");
                    }
                    else
                    {
                        moves[2] = new Point(99, 99);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.WriteLine("can't move" + PosibleI + " " + PosibleJ + "trow in move 3" +e.StackTrace);
            }

            //move 4(0,(j-2))
            PosibleI = i;
            PosibleJ = j - 2;
            try
            {
                IPieceToJump = Math.Abs((i + PosibleI) / 2);
                JPieceToJump = Math.Abs((j + PosibleJ) / 2);
                if (PosibleJ >= 0)
                {
                    if (boardArray[PosibleI][PosibleJ] == false && boardArray[IPieceToJump][JPieceToJump] == true)
                    {
                        //move is posible
                        moves[3] = new Point(PosibleI, PosibleJ);
                        canMove = true;
                        // Debug.WriteLine("can move");
                    }
                    else
                    {
                        moves[3] = new Point(99, 99);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.WriteLine("can't move" + PosibleI + " " + PosibleJ + "trow in move 4"+e.StackTrace);
            }

            //move 5(i+2,j+2))
            PosibleI = i + 2;
            PosibleJ = j + 2;
            try
            {
                IPieceToJump = Math.Abs((i + PosibleI) / 2);
                JPieceToJump = Math.Abs((j + PosibleJ) / 2);
                if (PosibleI <= (boardSize - 1) && PosibleJ <= (boardSize - 1))
                {
                    if (boardArray[PosibleI][PosibleJ] == false && boardArray[IPieceToJump][JPieceToJump] == true)
                    {
                        //move is posible
                        moves[4] = new Point(PosibleI, PosibleJ);
                        canMove = true;
                        // Debug.WriteLine("can move");
                    }
                    else
                    {
                        moves[4] = new Point(99, 99);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.WriteLine("can't move" + PosibleI + " " + PosibleJ + "trow in move 5" + e.StackTrace +e.StackTrace);
            }

            //move 6 (i-2,j-2)
            PosibleI = i - 2;
            PosibleJ = j - 2;
            try
            {
                IPieceToJump = Math.Abs((i + PosibleI) / 2);
                JPieceToJump = Math.Abs((j + PosibleJ) / 2);
                if (PosibleI >= 0 && PosibleJ >= 0)
                {
                    if (boardArray[PosibleI][PosibleJ] == false && boardArray[IPieceToJump][JPieceToJump] == true)
                    {
                        //move is posible
                        moves[5] = new Point(PosibleI, PosibleJ);
                        canMove = true;
                        //Debug.WriteLine("can move");
                    }
                    else
                    {
                        moves[5] = new Point(99, 99);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.WriteLine("can't move" + PosibleI + " " + PosibleJ + "trow in move 6" + e.StackTrace);
            }
            return canMove;
        }
        public bool isGameOver()
        {
            Point[] moves = new Point[6];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (boardArray[i][j] == true)
                    {
                        if (CheckIfCanMove(i, j, moves))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public static int getPiecesLeft(int boardSize, int piecesRemoved)
        {
            int totalPiece5 = 14;
            int totalPiece7 = 27;
            int totalPiece9 = 44;
            if (boardSize == 5)
            {
                return totalPiece5 - piecesRemoved+1;

            }
            else if (boardSize == 7)
            {
                return totalPiece7 - piecesRemoved+1;

            }
            else if (boardSize == 9)
            {
                return totalPiece9 - piecesRemoved+1;

            }
            return -1;
        }
        public static int getScore(int timeInSeconds, int boardSize, int piecesRemoved)
        {
            int totalPiece5 = 14;
            int totalPiece7 = 27;
            int totalPiece9 = 44;
            int piecesLeft = 0;
            int timeScore = 0;
            int score = 0;
            if (boardSize == 5)
            {
                piecesLeft = totalPiece5 - piecesRemoved+1;
                Debug.WriteLine(piecesLeft);
                if (piecesLeft == 1) score += 1100;

                timeScore = 300;
                score += piecesRemoved * 20;
            }
            else if (boardSize == 7)
            {
                piecesLeft = totalPiece7 - piecesRemoved+1;
                if (piecesLeft == 1) score += 2000;
                else if (piecesLeft == 2) score += 1000;
                score += piecesRemoved * 15;
                timeScore = 400;
            }
            else if (boardSize == 9)
            {
                piecesLeft = totalPiece9 - piecesRemoved+1;
                timeScore = 500;
                if (piecesLeft == 1) score += 3500;
                else if (piecesLeft == 2) score += 1500;
                else if (piecesLeft == 3) score += 100;
                score += piecesRemoved * 10;
            }
            if (timeScore - (timeInSeconds * 1.5) > 0)
            {
                score += (int)(timeScore - (timeInSeconds * 1.5));

            }
            return score;
        }
        #endregion
        #region Buttons Events methods
        private void PieceTappedFirstTime(object sender, TappedRoutedEventArgs e)
        {


            //get piece that triger action
            Ellipse piece = (Ellipse)sender;

            String PieceName = piece.Name;
            //get focus ring 
            String FocusRingName = "FocusCircle_" + PieceName.Substring(5, 3);
            Ellipse FocusRing = (Ellipse)FindName(FocusRingName);


            //get cordinates of piece
            int IFromEllipseTaped = Convert.ToInt32(PieceName.Substring(5, 1));
            int JFromEllipseTapped = Convert.ToInt32(PieceName.Substring(7, 1));

            //posible moves point (99,99) will be invalid move
            Point[] moves = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                moves[i] = new Point(99, 99);
            }

            if (CheckIfCanMove(IFromEllipseTaped, JFromEllipseTapped, moves))//if hera have a valid move
            {
                //sound peg tapped
                if (App.isSound) pegTapped.Play();

                //make any focus ring tapped befor not visible
                for (int i = 0; i < boardSize; i++)
                {
                    //get grid
                    String GridName = "BackGrid_" + i;
                    Grid BackGrid = (Grid)FindName(GridName);

                    for (int j = 0; j < (i + 1); j++)
                    {
                        String FocusRingNameToCollapse = "FocusCircle_" + i + "_" + j;
                        Ellipse FocusRingToCollapse = (Ellipse)FindName(FocusRingNameToCollapse);
                        FocusRingToCollapse.Visibility = Visibility.Collapsed;
                    }
                }//remove focus ring

                //if can move make focus ring visible
                FocusRing.Visibility = Visibility.Visible;

                Point invalidPoint = new Point(99, 99);
                foreach (Point point in moves)
                {
                    if (point != invalidPoint)
                    {
                        int iMove = (int)point.X;
                        int jMove = (int)point.Y;
                        //get holder to move
                        String PieceHolderToMoveName = "PieceHolder" + iMove + "_" + jMove;
                        Ellipse PieceHolderToMove = (Ellipse)FindName(PieceHolderToMoveName);

                        //add listener to move
                        //remove listener befor ading just in case piece get double click
                        PieceHolderToMove.Tapped -= PieceHolderToMove_Tapped;
                        PieceHolderToMove.Tapped += PieceHolderToMove_Tapped;
                        //need to send the pice that is moving
                        pieceToMove = new Point(IFromEllipseTaped, JFromEllipseTapped);
                    }
                }
            }//if can move 
            else//if cant move play other sound
            {
                if (App.isSound) brongTap.Play();
            }
        }//PieceTapped

        private void PieceHolderToMove_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //add sound for correct move
            if (App.isSound) pegJumped.Play();

            //remove piece to move
            RemovePiece((int)pieceToMove.X, (int)pieceToMove.Y);
            //hide focuse ring
            Ellipse FocusCircle = (Ellipse)FindName("FocusCircle_" + pieceToMove.X + "_" + pieceToMove.Y);
            FocusCircle.Visibility = Visibility.Collapsed;

            //remove jumped piece
            Ellipse HolderToMove = (Ellipse)sender;
            int IPosToMove = Convert.ToInt32(HolderToMove.Name.Substring(11, 1));
            int JPosToMove = Convert.ToInt32(HolderToMove.Name.Substring(13, 1));

            int IPieceJumped, JPieceJumped;
            IPieceJumped = Math.Abs(((int)pieceToMove.X + IPosToMove) / 2);
            JPieceJumped = Math.Abs(((int)pieceToMove.Y + JPosToMove) / 2);

            RemovePiece(IPieceJumped, JPieceJumped);

            //ad piece
            AddPiece(IPosToMove, JPosToMove);

            //remove listener from sender
            HolderToMove.Tapped -= PieceHolderToMove_Tapped;
            String HolderName;
            Ellipse PieceHolder;
            for (int i = 0; i < boardSize; i++)
            {
                //get grid
                String GridName = "BackGrid_" + i;
                Grid BackGrid = (Grid)FindName(GridName);

                for (int j = 0; j < (i + 1); j++)
                {
                    HolderName = "PieceHolder" + i + "_" + j;
                    PieceHolder = (Ellipse)FindName(HolderName);
                    PieceHolder.Tapped -= PieceHolderToMove_Tapped; ;

                }
            }
            piecesRemoved++;

            if (isGameOver())
            {
                gamePage.GameOverAsync();
            }

        }//PieceHolderTo move

        #endregion

    }
}
