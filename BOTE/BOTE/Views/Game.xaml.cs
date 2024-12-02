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
    public partial class Game : Window
    {
        //Help Text
        public string StatusText { get; set; }

        //Fields in possession
        public int NumOfFields1 { get; set; } = 4;
        public int NumOfFields2 { get; set; } = 5;

        //Player 1 units
        public int NumOfInfantry1 { get; set; } = 0;
        public int NumOfCavalry1 { get; set; } = 0;
        public int NumOfCannons1 { get; set; } = 0;

        //Player 1 units
        public int NumOfInfantry2 { get; set; } = 0;
        public int NumOfCavalry2 { get; set; } = 0;
        public int NumOfCannons2 { get; set; } = 0;

        //Battlefield units
        public int[,] BattlefieldUnits { get; set; } = new int[9, 3];

        //img src
        public string Player1ImageSourcePath { get; set; }
        public string Player2ImageSourcePath { get; set; }

        //battlefield
        private Button[,] Battlefield;

        //teritories
        private List<Button> Player1Fields = new List<Button>();
        private List<Button> Player2Fields = new List<Button>();

        //game flags
        public bool FirstPlayerTurn {  get; set; } = true;
        public bool CardPullPhase { get; set; } = true;

        public bool SelectingReinforcementsField { get; set; } = false;
        public bool SelectAttackField { get; set; } = false;
        public bool SelectDefendField { get; set; } = false;
        public bool AttackPhase { get; set; } = false;
        public bool SelectOpponentField { get; set; } = false;
        public bool NextMove {  get; set; } = false;
        
        //temp
        public int NumOfUnits { get; set; } = 0;
        public int TroopId { get; set; } = 0;
        public Button AttackField { get; set; }
        public Button DefendField { get; set; }

        public int AttackFX { get; set; }
        public int AttackFY { get; set; }
        public int DefendFX { get; set; }
        public int DefendFY { get; set; }

        //Card images
        public string Knight = "knight";
        public string Cavalry = "cavalry";
        public string Catapult = "catapult";
        public string ImageExtension = ".png";

        Player Player1 = (Player)Application.Current.Resources["Player1"];
        Player Player2 = (Player)Application.Current.Resources["Player2"];
        public Game()
        {
            InitializeComponent();

            Player1ImageSourcePath = Player1.Image;
            Player2ImageSourcePath = Player2.Image;

            Battlefield = new Button[,] { { Field0, Field1, Field2 }, { Field3, Field4, Field5 }, { Field6, Field7, Field8 } };

            var player1Brush = new BrushConverter().ConvertFromString(Player1.Color) as Brush;
            var player2Brush = new BrushConverter().ConvertFromString(Player2.Color) as Brush;

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
                            Player1Fields.Add(Battlefield[i, j]);
                        }
                        else
                        {
                            Battlefield[i, j].Background = player2Brush;
                            Battlefield[i, j].IsEnabled = false;
                            Player2Fields.Add(Battlefield[i, j]);
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
            int typeOfUnit = random.Next(10);
            if (FirstPlayerTurn)
            {
                NumOfUnits = random.Next(1, Player1Fields.Count);
                if (typeOfUnit < 5)
                {
                    NumOfInfantry1 += NumOfUnits;
                    TroopId = 0;
                }
                else if (typeOfUnit >= 5 && typeOfUnit < 8)
                {
                    NumOfCavalry1 += NumOfUnits;
                    TroopId = 1;
                }
                else 
                { 
                    NumOfCannons1 += NumOfUnits;
                    TroopId = 2;
                }

                foreach (Button field in Player1Fields)
                {
                    field.IsEnabled = true;
                }
            }
            else
            {
                NumOfUnits = random.Next(1, Player2Fields.Count);
                if (typeOfUnit < 5)
                {
                    NumOfInfantry2 += NumOfUnits;
                    TroopId = 0;
                }
                else if (typeOfUnit >= 5 && typeOfUnit < 8)
                {
                    NumOfCavalry2 += NumOfUnits;
                    TroopId = 1;
                }
                else
                {
                    NumOfCannons2 += NumOfUnits;
                    TroopId = 2;
                }

                foreach (Button field in Player2Fields)
                {
                    field.IsEnabled = true;
                }
            }

            Card card;

            if(TroopId == 0)
            {
                card = new Card(Knight + ImageExtension, Knight + " x " + NumOfUnits);
            }
            else if(TroopId == 1)
            {
                card = new Card(Cavalry + ImageExtension, Cavalry + " x " + NumOfUnits);
            }
            else
            {
                card = new Card(Catapult + ImageExtension, Catapult + " x " + NumOfUnits);
            }

            card.ShowDialog();

            StatusText = "Choose where to move troops";
            DrawCard.IsEnabled = false;

            SelectingReinforcementsField = true;

            Dispatcher.Invoke(() =>
            {
                this.DataContext = null;
                this.DataContext = this;
            });
        }

        private void onFieldClick(Object sender, RoutedEventArgs e)
        {
            Button field = (Button)sender;
            if (SelectingReinforcementsField)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (field.Name == Battlefield[i, j].Name)
                        {
                            BattlefieldUnits[i * 3 + j, TroopId] += NumOfUnits;
                            break;
                        }
                    }
                }
                SelectingReinforcementsField = false;
                SelectAttackField = true;
                StatusText = "Select your camp for battle";
            }
            else if (SelectAttackField)
            {
                AttackField = field;
                AttackField.BorderBrush = new BrushConverter().ConvertFromString("gold") as Brush;
                AttackField.BorderThickness = new Thickness(6);
                SelectAttackField = false;

                StatusText = "Select target field";
                if (FirstPlayerTurn)
                {
                    foreach (Button fld in Player1Fields)
                    {
                        fld.IsEnabled = false;
                    }
                }
                else
                {
                    foreach (Button fld in Player2Fields)
                    {
                        fld.IsEnabled = false;
                    }
                }
                int x = 0, y = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Battlefield[i, j] == AttackField)
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
                            if (FirstPlayerTurn)
                            {
                                if (Player2Fields.Contains(Battlefield[i, j]))
                                {
                                    Battlefield[i, j].IsEnabled = true;
                                    counter++;
                                }
                            }
                            else
                            {
                                if (Player1Fields.Contains(Battlefield[i, j]))
                                {
                                    Battlefield[i, j].IsEnabled = true;
                                    counter++;
                                }
                            }
                        }
                    }
                }

                if (counter > 0)
                {
                    SelectDefendField = true;
                }
            }
            else if (SelectDefendField)
            {
                DefendField = field;
                DefendField.BorderBrush = new BrushConverter().ConvertFromString("gold") as Brush;
                DefendField.BorderThickness = new Thickness(6);
                SelectDefendField = false;
                AttackPhase = true;
                Attack.IsEnabled = true;
            }

            Dispatcher.Invoke(() =>
            {
                this.DataContext = null;
                this.DataContext = this;
            });
        }

        private void AttackBtn(Object sender, RoutedEventArgs e)
        {
            if (AttackPhase)
            { 
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Battlefield[i, j] == DefendField)
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

                int index1 = AttackFX * 3 + AttackFY;
                int index2 = DefendFX * 3 + DefendFY;

                if (FirstPlayerTurn)
                {
                    int player1Points = randFact1 + BattlefieldUnits[index1, 2] * 3 + BattlefieldUnits[index1, 1] * 2 + BattlefieldUnits[index1, 0];
                    int player2Points = randFact2 + BattlefieldUnits[index2, 2] * 3 + BattlefieldUnits[index2, 1] * 2 + BattlefieldUnits[index2, 0];
                    StatusText = "Player 1: " + player1Points + "(" + randFact1 + ") - Player 2: " + player2Points + "(" + randFact2 + ")";
                    if (player1Points > player2Points)
                    {
                        Player2Fields.Remove(DefendField);
                        Player1Fields.Add(DefendField);
                        var player1Brush = new BrushConverter().ConvertFromString(Player1.Color) as Brush;
                        DefendField.Background = player1Brush;
                        for (int i = 0; i < 3; i++)
                        {
                            BattlefieldUnits[index2, i] /= 2;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            BattlefieldUnits[index1, i] /= 2;
                        }
                    }
                }
                else
                {
                    int player1Points = randFact1 + BattlefieldUnits[index2, 2] * 3 + BattlefieldUnits[index2, 1] * 2 + BattlefieldUnits[index2, 0];
                    int player2Points = randFact2 + BattlefieldUnits[index1, 2] * 3 + BattlefieldUnits[index1, 1] * 2 + BattlefieldUnits[index1, 0];
                    StatusText = "Player 1: " + player1Points + "(" + randFact1 + ") - Player 2: " + player2Points + "(" + randFact2 + ")";
                    if (player2Points > player1Points)
                    {
                        Player1Fields.Remove(DefendField);
                        Player2Fields.Add(DefendField);
                        var player2Brush = new BrushConverter().ConvertFromString(Player2.Color) as Brush;
                        DefendField.Background = player2Brush;
                        for (int i = 0; i < 3; i++)
                        {
                            BattlefieldUnits[index2, i] /= 2;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            BattlefieldUnits[index1, i] /= 2;
                        }
                    }
                }
                AttackPhase = false;
                NextMove = true;
                Attack.IsEnabled = false;
                NextTurn.IsEnabled = true;

                NumOfFields1 = Player1Fields.Count;
                NumOfFields2 = Player2Fields.Count;

                Dispatcher.Invoke(() =>
                {
                    this.DataContext = null;
                    this.DataContext = this;
                });
            }
        }

        private void NextTurnBtn(Object sender, RoutedEventArgs e)
        {
            if (NextMove)
            {
                DefendField.BorderBrush = new BrushConverter().ConvertFromString("white") as Brush;
                DefendField.BorderThickness = new Thickness(1);
                AttackField.BorderBrush = new BrushConverter().ConvertFromString("white") as Brush;
                AttackField.BorderThickness = new Thickness(1);
                NextMove = false;
                NextTurn.IsEnabled = false;
                DrawCard.IsEnabled = true;
                foreach (var item in Battlefield)
                {
                    item.IsEnabled = false;
                }
                if(FirstPlayerTurn)
                {
                    FirstPlayerTurn = false;
                }
                else
                {
                    FirstPlayerTurn = true;
                }
                if(Player1Fields.Count == 0)
                {
                    StatusText = "Player 2 Wins";
                    DrawCard.IsEnabled = false;
                }
                else if(Player2Fields.Count == 0)
                {
                    StatusText = "Player 1 Wins";
                    DrawCard.IsEnabled = false;
                }
            }

            Dispatcher.Invoke(() =>
            {
                this.DataContext = null;
                this.DataContext = this;
            });
        }
    }
}
