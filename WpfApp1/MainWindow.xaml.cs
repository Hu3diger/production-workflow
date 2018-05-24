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
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        Object principal;
        Page1 page1;
        public MainWindow()
        {
            InitializeComponent();

            page1 = new Page1(300, this);
            page1.Background = Brushes.BlueViolet;
            
            principal = this.Content;
        }

        private void goToRegister_Click(object sender, RoutedEventArgs e)
        {
            Register rg = new Register();
            this.Close ();
            rg.Show();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void gotoProcess_Click(object sender, RoutedEventArgs e)
        {
            this.Content = page1;
        }

        public void goBack()
        {
            this.Content = principal;
            TextBlock txt = new TextBlock();
            txt.Text = page1.apareceAqui.Text;
            txt.Foreground = new SolidColorBrush(Colors.Yellow);
            TextBlock txt1 = new TextBlock();
            txt1.Text = page1.apareceAqui.Text;
            txt1.Foreground = new SolidColorBrush(Colors.Pink);
            StackPanel stck = new StackPanel();
            stck.Children.Add(txt);
            stck.Children.Add(txt1);
            this.viewer.Content = stck;
        }

        /*
        private void Teste_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Window();
            win.Height = 150;
            TextBox txtBox = new TextBox();
            txtBox.Text = "Teste";
            StackPanel stck = new StackPanel();
            Button btn = new Button();
            btn.Width = 200;
            btn.Height = 200;
            stck.Children.Add(txtBox);
            stck.Children.Add(btn);
            //Border myborder = new Border();
            win.Content = stck;
            win.Show();
        }
        */
    }
}
