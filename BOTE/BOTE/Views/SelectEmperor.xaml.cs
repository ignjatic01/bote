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
        public string imagePath = "pack://application:,,,/Images/";
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
                        player1.Name = "Baldwin IV";
                        player1.Color = "#3f2d47";
                        player1.Image = imagePath + "baldwin.png";
                    }
                    else if (button.Name == "DusanButton")
                    {
                        player1.Name = "Tsar Dušan";
                        player1.Color = "#731111";
                        player1.Image = imagePath + "dusan.png";
                    }
                    else if (button.Name == "SaladinButton")
                    {
                        player1.Name = "Salah ad-Din";
                        player1.Color = "#255421";
                        player1.Image = imagePath + "saladin.png";
                    }
                    else if (button.Name == "NevskyButton")
                    {
                        player1.Name = "Nevsky";
                        player1.Color = "#66641e";
                        player1.Image = imagePath + "nevsky.png";
                    }

                    SelectLabel.Text = "Player 2 select";
                }
                else
                {
                    player2.Selected = true;
                    if (button.Name == "BaldwinButton")
                    {
                        player2.Name = "Baldwin IV";
                        player2.Color = "#3f2d47";
                        player2.Image = imagePath + "baldwin.png";
                    }
                    else if (button.Name == "DusanButton")
                    {
                        player2.Name = "Tsar Dušan";
                        player2.Color = "#731111";
                        player2.Image = imagePath + "dusan.png";
                    }
                    else if (button.Name == "SaladinButton")
                    {
                        player2.Name = "Salah ad-Din";
                        player2.Color = "#255421";
                        player2.Image = imagePath + "saladin.png";
                    }
                    else if (button.Name == "NevskyButton")
                    {
                        player2.Name = "Nevsky";
                        player2.Color = "#66641e";
                        player2.Image = imagePath + "nevsky.png";
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
