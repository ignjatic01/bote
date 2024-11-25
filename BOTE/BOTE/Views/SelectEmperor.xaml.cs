using BOTE.Models;
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
using System.Windows.Shapes;

namespace BOTE.Views
{
    /// <summary>
    /// Interaction logic for SelectEmperor.xaml
    /// </summary>
    public partial class SelectEmperor : Window
    {
        public SelectEmperor()
        {
            InitializeComponent();
        }

        private void EmperorSelected(Object sender, RoutedEventArgs e)
        {
            Player player1 = (Player)Application.Current.Resources["Player1"];
            Player player2 = (Player)Application.Current.Resources["Player2"];

            Button button = (Button)sender;
            button.IsEnabled = false;

            if (player1 != null && player2 != null)
            {
                if (!player1.Selected)
                {
                    player1.Selected = true;
                    if (button.Name == "BaldwinButton")
                    {
                        player1.Name = "King Baldwin IV";
                        player1.Color = "#3f2d47";
                    }
                    else if (button.Name == "DusanButton")
                    {
                        player1.Name = "Tsar Stefan IV Dušan";
                        player1.Color = "#731111";
                    }
                    else if (button.Name == "SaladinButton")
                    {
                        player1.Name = "Sultan Salah ad-Din";
                        player1.Color = "#255421";
                    }

                    SelectLabel.Text = "Player 2 select";
                }
                else
                {
                    player2.Selected = true;
                    if (button.Name == "BaldwinButton")
                    {
                        player2.Name = "King Baldwin IV";
                        player2.Color = "#3f2d47";
                    }
                    else if (button.Name == "DusanButton")
                    {
                        player2.Name = "Tsar Stefan IV Dušan";
                        player2.Color = "#731111";
                    }
                    else if (button.Name == "SaladinButton")
                    {
                        player2.Name = "Sultan Salah ad-Din";
                        player2.Color = "#255421";
                    }
                    
                    Game game = new Game();
                    Application.Current.MainWindow = game;
                    this.Close();
                    game.Show();
                }
            }
        }
    }
}
