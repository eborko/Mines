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

namespace MinesGame
{
    public delegate void FieldDicoveredHandler(object sender);
    /// <summary>
    /// Interaction logic for Field.xaml
    /// </summary>
    public partial class Field : UserControl
    {
        public event FieldDicoveredHandler FieldDiscovered;
        public Field LeftField { get; set; }
        public Field TopLeftField { get; set; }
        public Field TopField { get; set; }
        public Field TopRightField { get; set; }
        public Field RightField { get; set; }
        public Field BottomRightField { get; set; }
        public Field BottomField { get; set; }
        public Field BottomLeftField { get; set; }

        public Field()
        {
            InitializeComponent();
            MainButton.Click += MainButton_Click;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            MainButton.Visibility = Visibility.Hidden;
            MainButton.Click -= MainButton_Click;
            FieldDiscovered?.Invoke(this);
        }
    }
}
