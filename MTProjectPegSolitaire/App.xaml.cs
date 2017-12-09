using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


/*
 *   Jose Ignacio Retamal Peg Solitaire Game .
 *   17/11/2017 File created: Jose Ignacio Retamal.
 *  
 */

namespace MTProjectPegSolitaire
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// 
        /// </summary>
        //app variables
        #region app variables
        public static int lastBoardSize { get; set; }
        public static int lastTotalTimeSecond { get; set; }
        public static String lastTotalTime { get; set; }
        public static int lastPiecesRemoved { get; set; }
        public static int lastPiecesLeft { get; set; }
        public static int lastScore { get; set; }
        
        //highScore
        public static bool isHighScoreChecked { get; set; }

        public static bool continueGame { get; set; }
        public static int piecesRemovedForContinue { get; set; }

        //0 high score 1, 1 high 1 and 2 lowe high
        public static int[] highScores { get; set; }
        public static String[] highScoresName { get; set; }

        //game state
        public static Board board { get; set; }
        public static TimeKeeper timer { get; set; }

        //check if is on gameover page for not save game when close
        public static bool isOnGameOverPage { get; set; }

        //random for use in the full app
        public static Random random { get; set; }
        #endregion
        public App()
        {
            
                                   this.InitializeComponent();
            this.initAppVariables();

            //add on loadig for local setting
            this.Suspending += OnSuspending;
            //prefered size
            ApplicationView.PreferredLaunchViewSize = new Size(700, 750);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }
        private void initAppVariables()
        {
            lastBoardSize = 5;
            lastTotalTimeSecond = 0;
            lastTotalTime = "";
            lastPiecesRemoved = 0;
            lastPiecesLeft = 0;
            lastScore = 0;
            isHighScoreChecked = false;
            continueGame = false;
            piecesRemovedForContinue = 0;
            piecesRemovedForContinue = 0;
            highScores = new int[3];
            highScoresName = new String[3];
            isOnGameOverPage = false;
            random = new Random();
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            try
            {
                if (!App.isOnGameOverPage)
                {
                    localSettings.Values["boardArray"] = App.board.getBoardArrayInOneString();

                    localSettings.Values["LastTime"] = App.timer.GetTotalSeconds();
                    localSettings.Values["LastPiecesRemoves"] = App.board.GetPieceRemoved();
                }
            }
            catch (Exception exp2)
            {

            }
        }
    }
}
