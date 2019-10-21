using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameView : Window
    {
        private GameGrid gameGrid;
        public GameView()
        {
            InitializeComponent();
            gameGrid = new GameGrid();
            this.DataContext = gameGrid;
        }


        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            gameGrid.Merge(Direction.UP);
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            gameGrid.Merge(Direction.DOWN);
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            gameGrid.Merge(Direction.LEFT);
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            gameGrid.Merge(Direction.RIGHT);
        }
    }
}
