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

        //Help Text
        public string StatusText { get; set; }

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

        //img src
        public string Player1ImageSourcePath { get; set; }
        public string Player2ImageSourcePath { get; set; }

        //battlefield
        private Button[,] Battlefield;

        //teritories
        private List<Button> player1Fields = new List<Button>();
        private List<Button> player2Fields = new List<Button>();

        //game flags
        public bool firstPlayerTurn {  get; set; } = true;
        public bool cardPullPhase { get; set; } = true;

        public bool selectingReinforcementsField { get; set; } = false;
        public bool selectAttackField { get; set; } = false;
        public bool selectDefendField { get; set; } = false;
        public bool attackPhase { get; set; } = false;
        public bool selectOpponentField { get; set; } = false;
        public bool nextMove {  get; set; } = false;
        
        //temp
        public int numOfUnits { get; set; } = 0;
        public int troopId { get; set; } = 0;
        public Button attackField { get; set; }
        public Button defendField { get; set; }

        public int AttackFX { get; set; }
        public int AttackFY { get; set; }
        public int DefendFX { get; set; }
        public int DefendFY { get; set; }
        public Game()
        {
            InitializeComponent();
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

            Battlefield = new Button[,] { { Field0, Field1, Field2 }, { Field3, Field4, Field5 }, { Field6, Field7, Field8 } };

            var player1Brush = new BrushConverter().ConvertFromString(player1.Color) as Brush;
            var player2Brush = new BrushConverter().ConvertFromString(player2.Color) as Brush;

            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                { 
                    if (Battlefield[i, j] != null)
                    {
                        if (k < 4)
                        {
                            Battlefield[i, j].Background = player1Brush;
                            Battlefield[i, j].IsEnabled = false;
                            player1Fields.Add(Battlefield[i, j]);
                        }
                        else
                        {
                            Battlefield[i, j].Background = player2Brush;
                            Battlefield[i, j].IsEnabled = false;
                            player2Fields.Add(Battlefield[i, j]);
                        }
                    }

                    k++;
                }
            }

            Attack.IsEnabled = false;
            NextTurn.IsEnabled = false;

            StatusText = "Draw a Card (Player 1)";

            DataContext = this;
        }

        private void DrawCardEvent(Object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            numOfUnits = random.Next(1, 4);
            int typeOfUnit = random.Next(10);
            if (firstPlayerTurn)
            {
                if (typeOfUnit < 5)
                {
                    numOfInfantry1 += numOfUnits;
                    troopId = 0;
                }
                else if (typeOfUnit >= 5 && typeOfUnit < 8)
                {
                    numOfCavalry1 += numOfUnits;
                    troopId = 1;
                }
                else 
                { 
                    numOfCannons1 += numOfUnits;
                    troopId = 2;
                }

                foreach (Button field in player1Fields)
                {
                    field.IsEnabled = true;
                }
            }
            else
            {
                if (typeOfUnit < 5)
                {
                    numOfInfantry2 += numOfUnits;
                }
                else if (typeOfUnit >= 5 && typeOfUnit < 8)
                {
                    numOfCavalry2 += numOfUnits;
                }
                else
                {
                    numOfCannons2 += numOfUnits;
                }

                foreach (Button field in player2Fields)
                {
                    field.IsEnabled = true;
                }
            }

            StatusText = "Choose where to move troops";
            DrawCard.IsEnabled = false;

            selectingReinforcementsField = true;

            Dispatcher.Invoke(() =>
            {
                // Ovdje se mogu vršiti promene koje trebaju biti odražene u UI-u
                this.DataContext = null;
                this.DataContext = this;
            });
        }

        private void onFieldClick(Object sender, RoutedEventArgs e)
        {
            Button field = (Button)sender;
            if(selectingReinforcementsField)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (field.Name == Battlefield[i, j].Name)
                        {
                            BattlefieldUnits[i * 3 + j, troopId] += numOfUnits;
                            break;
                        }
                    }
                }
                selectingReinforcementsField = false;
                selectAttackField = true;
                StatusText = "Select your camp for battle"; 
            }
            else if(selectAttackField)
            {
                attackField = field;
                attackField.BorderBrush = new BrushConverter().ConvertFromString("gold") as Brush;
                attackField.BorderThickness = new Thickness(6);
                selectAttackField = false;
                
                StatusText = "Select target field";
                if(firstPlayerTurn)
                {
                    foreach (Button fld in player1Fields)
                    {
                        fld.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (Button fld in player2Fields)
                    {
                        fld.IsEnabled = false;
                    }
                }
                int x = 0, y = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Battlefield[i, j] == attackField)
                        {
                            AttackFX = x = i;
                            AttackFY = y = j;
                            break;
                        }
                    }
                }

                int counter = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((i - 1 == x && j == y) || (i + 1 == x && j == y) || (j - 1 == y && i == x) || (j + 1 == y && i == x))
                        {
                            if(firstPlayerTurn)
                            {
                                if (player2Fields.Contains(Battlefield[i,j]))
                                {
                                    Battlefield[i, j].IsEnabled = true;
                                    counter++;
                                }
                            }
                            else
                            {
                                if(player1Fields.Contains(Battlefield[i,j]))
                                {
                                    Battlefield[i, j].IsEnabled = true;
                                    counter++;
                                }
                            }
                        }
                    }
                }

                if(counter > 0)
                {
                    selectDefendField = true;
                }
            }
            else if(selectDefendField)
            {
                defendField = field;
                defendField.BorderBrush = new BrushConverter().ConvertFromString("gold") as Brush;
                defendField.BorderThickness = new Thickness(6);
                selectDefendField = false;
                attackPhase = true;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Battlefield[i, j] == defendField)
                        {
                            DefendFX = i;
                            DefendFY = j;
                            break;
                        }
                    }
                }

                Random random = new Random();

                int randFact1 = random.Next(7);
                int randFact2 = random.Next(7);

                if (firstPlayerTurn)
                {
                    int index1 = AttackFX * 3 + AttackFY;
                    int index2 = DefendFX * 3 + DefendFY;
                    int player1Points = randFact1 + BattlefieldUnits[index1, 2] * 3 + BattlefieldUnits[index2, 1] * 2 + BattlefieldUnits[index1, 0];
                    int player2Points = randFact2 + BattlefieldUnits[index2, 2] * 3 + BattlefieldUnits[index2, 1] * 2 + BattlefieldUnits[index2, 0];
                    StatusText = "Player 1: " + player1Points + "(" + randFact1 + ") - Player 2: " + player2Points + "(" + randFact2 + ")";
                }
                else
                {

                }
            }

            Dispatcher.Invoke(() =>
            {
                // Ovdje se mogu vršiti promene koje trebaju biti odražene u UI-u
                this.DataContext = null;
                this.DataContext = this;
            });
        }
    }
}
