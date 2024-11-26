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
    /// Interaction logic for game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public string imagePath = "pack://application:,,,/Images/";
        public string baldwinName = "Baldwin IV";
        public string dusanName = "Tsar Dušan";
        public string saladinName = "Salah ad-Din";
        public string nevskyName = "Nevsky";

        //Fields in possession
        public int numOfFields1 { get; set; } = 4;
        public int numOfFields2 { get; set; } = 5;

        //Player 1 units
        public int numOfInfantry1 { get; set; } = 0;
        public int numOfCavalry1 { get; set; } = 0;
        public int numOfCannons1 { get; set; } = 0;

        //Player 1 units
        public int numOfInfantry2 { get; set; } = 0;
        public int numOfCavalry2 { get; set; } = 0;
        public int numOfCannons2 { get; set; } = 0;

        //Battlefield units
        public int[,] BattlefieldUnits { get; set; } = new int[9, 3];

        public string Player1ImageSourcePath { get; set; }
        public string Player2ImageSourcePath { get; set; }
        public Game()
        {
            Player player1 = (Player)Application.Current.Resources["Player1"];
            Player player2 = (Player)Application.Current.Resources["Player2"];

            if (player1.Name == baldwinName)
            {
                Player1ImageSourcePath = imagePath + "baldwin.png";
            }
            else if (player1.Name.Equals(dusanName))
            {
                Player1ImageSourcePath = imagePath + "dusan.png";
            }
            else if (player1.Name == saladinName)
            {
                Player1ImageSourcePath = imagePath + "saladin.png";
            }
            else if(player1.Name == nevskyName)
            {
                Player1ImageSourcePath = imagePath + "nevsky.png";
            }

            if (player2.Name == baldwinName)
            {
                Player2ImageSourcePath = imagePath + "baldwin.png";
            }
            else if (player2.Name == dusanName)
            {
                Player2ImageSourcePath = imagePath + "dusan.png";
            }
            else if (player2.Name == saladinName)
            {
                Player2ImageSourcePath = imagePath + "saladin.png";
            }
            else if (player2.Name == nevskyName)
            {
                Player2ImageSourcePath = imagePath + "nevsky.png";
            }
            
            DataContext = this;
            InitializeComponent();
        }
    }
}
