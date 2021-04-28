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
using VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_WPF
{
    /// <summary>
    /// Interaction logic for Beroemdheden.xaml
    /// </summary>
    public partial class Beroemdheden : Window
    {
        public Beroemdheden()
        {
            InitializeComponent();
        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataBeroemdheden.ItemsSource = DatabaseOperations.OphalenBeroemdheden();
        }

        private void dataBeroemdheden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataBeroemdheden.SelectedItem is Beroemdheid beroemdheid)
            {
                cmbNationaliteit.SelectedItem = beroemdheid.Nationaliteit;
            }
        }
    }
}
