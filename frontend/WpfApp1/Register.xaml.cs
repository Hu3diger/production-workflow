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
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            //MessageBox.Show($"A descrição é {this.DescriptionText.Text}");
            this.numPeca.Text = "" + rnd.Next(0, 1001);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.WeldCheckbox.IsChecked = this.AssemblyCheckbox.IsChecked = this.PlasmaCheckbox.IsChecked = this.LaserCheckbox.IsChecked = this.PurchaseCheckbox.IsChecked =
                this.LatheCheckbox.IsChecked = this.DrillCheckbox.IsChecked = this.FoldCheckbox.IsChecked = this.RollCheckbox.IsChecked = this.SawCheckbox.IsChecked = false;

            this.numPeca.Clear();
            this.DescriptionText.Clear();
            this.LenghtText.Clear();
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            this.LenghtText.Text += ((CheckBox)sender).Content + " - ";
        }

        private void FinishDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NoteText == null)
            {
                return;
            }

            this.NoteText.Text = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FinishDropdown_SelectionChanged(this.FinishDropdown, null);
        }

        private void SupplierNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.MassText.Text = this.SupplierNameText.Text;
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
