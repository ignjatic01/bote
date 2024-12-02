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
    /// Interaction logic for Card.xaml
    /// </summary>
    public partial class Card : Window
    {
        public string imagePath = "pack://application:,,,/Images/";
        public string CardImage { get; set; }
        public Card(string imageName, string text)
        {
            InitializeComponent();

            CardImage = imagePath + imageName;
            Info.Text = text;

            DataContext = this;
        }
    }
}
