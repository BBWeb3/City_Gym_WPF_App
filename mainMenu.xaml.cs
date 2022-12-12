using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assess3T2
{
    /// <summary>
    /// Interaction logic for mainMenu.xaml
    /// </summary>
    public partial class mainMenu : Window
    {
        public mainMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Hide main menu
            mainMenu.GetWindow(this).Hide();

            //Load member registration form
            regForm newMember = new regForm();
            newMember.ShowDialog();

            //Show main menu
            mainMenu.GetWindow(this).Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Hide main menu
            mainMenu.GetWindow(this).Hide();

            //Load search members window
            searchMembers newSearch = new searchMembers();
            newSearch.ShowDialog();

            //Show main menu
            mainMenu.GetWindow(this).Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Hide main menu
            mainMenu.GetWindow(this).Hide();

            //Load search members window
            Window3 newSearch = new Window3();
            newSearch.ShowDialog();

            //Show main menu
            mainMenu.GetWindow(this).Show();
        }
    }
}
