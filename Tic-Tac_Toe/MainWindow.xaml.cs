using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ticTac = new TicTac();

            this.newGame();
        }

        #region privateMembers

        /// <summary>
        /// instance of <see cref="TicTac"/>
        /// </summary>
        private TicTac ticTac;
        /// <summary>
        /// initialise new game
        /// </summary> 

        private void newGame()
        {
            ticTac.defaultTileInit();

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
        }

        /// <summary>
        /// button click Event Handler....
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var gridIndex = column + (row * 3);

            if (ticTac.PlayerState && ticTac.GameState)
            {
                if (ticTac.tileValues[gridIndex] != TicTac.BoxState.free)
                {
                    MessageBox.Show("Already Filled!");
                }

                else
                {
                    ticTac.tileValues[gridIndex] = TicTac.BoxState.cross;
                    button.Content = 'X';
                    ticTac.PlayerState = false;
                }
                ticTac.getWinner(TicTac.BoxState.cross);
            }


            if (!ticTac.PlayerState && ticTac.GameState)
            {
                var btn = Container.Children.Cast<Button>().ToArray();
                var index = ticTac.computerPlay();
                btn[index].Content = 'O';
                btn[index].Foreground = Brushes.Red;
                ticTac.tileValues[index] = TicTac.BoxState.zero;
                ticTac.PlayerState = true;
                ticTac.getWinner(TicTac.BoxState.zero);
            }

            if (ticTac.GameState != true)
            {
                if (ticTac.winner == TicTac.IdentifyWinner.stalemate)
                {
                    Container.Children.Cast<Button>().ToList().ForEach(btn=>
                    {
                        button.Background = Brushes.Gold;
                        button.Foreground = Brushes.Black;
                    });

                    MessageBox.Show($"{ticTac.winner.ToString()}");
                    this.newGame();
                }

                if (ticTac.winner == TicTac.IdentifyWinner.player || ticTac.winner == TicTac.IdentifyWinner.computer)
                {
                    var btn = Container.Children.Cast<Button>().ToArray();

                    for (int i = 0; i < ticTac.maxRowSize; ++i)
                    {
                        btn[ticTac.winSegments[i]].Background = Brushes.Green;
                    }
                    MessageBox.Show($"{ticTac.winner.ToString()} wins GameOver");
                    this.newGame();
                }
            }
        }

        #endregion
    }
}
