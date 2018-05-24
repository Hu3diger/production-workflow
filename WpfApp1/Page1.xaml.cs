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


namespace WpfBasics
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        MainWindow _window;
        public Page1(int teste, MainWindow window)
        {
            InitializeComponent();
            this.Height = teste;
            _window = window;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _window.goBack();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            apareceAqui.Text = escreveAqui.Text;
        }
    }
}
