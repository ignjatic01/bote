using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Rules.xaml
    /// </summary>

    public partial class Rules : Window
    {
        public string Rule1 { get; set; } = "";
        public string Rule2 { get; set; } = "";
        public string Rule3 { get; set; } = "";
        public string Rule4 { get; set; } = "";
        public string FilePath = "Data/rules.txt";
        public Rules()
        {
            InitializeComponent();
            string[] lines;
            try
            {
                string fullPath = System.IO.Path.GetFullPath(FilePath);
                lines = File.ReadAllLines(fullPath);
                Rule1 = lines[0];
                Rule2 = lines[1];
                Rule3 = lines[2];
                Rule4 = lines[3];
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

            DataContext = this;
        }
    }
}
