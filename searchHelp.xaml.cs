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
    /// Interaction logic for searchHelp.xaml
    /// </summary>
    public partial class searchHelp : Window
    {
        public searchHelp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Hide  searchHelp Window
            searchHelp.GetWindow(this).Close();

           
        }
    }
}
