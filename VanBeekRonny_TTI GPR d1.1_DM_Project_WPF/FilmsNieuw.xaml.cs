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
    /// Interaction logic for FilmsNieuw.xaml
    /// </summary>
    public partial class FilmsNieuw : Window
    {
        public FilmsNieuw()
        {
            InitializeComponent();
        }

        //Globale variabelen
        List<Beroemdheid> cast = new List<Beroemdheid>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbTaal.ItemsSource = DatabaseOperations.Talen();
            cmbLeeftijdsgroep.ItemsSource = DatabaseOperations.Leeftijdsgroepen();
            cmbBeroemdheden.ItemsSource = DatabaseOperations.OphalenBeroemdheden();
            cmbGenres.ItemsSource = DatabaseOperations.OphalenGenres();
            lbCast.ItemsSource = cast;
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBeroemdheden.SelectedItem is Beroemdheid beroemdheid)
            {
                if (!cast.Contains(beroemdheid))
                {
                    cast.Add(beroemdheid);
                    lbCast.Items.Refresh();
                }
                else
                {
                    MessageBox.Show($"{beroemdheid.voornaam} {beroemdheid.naam} staat al in de lijst.");
                }
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lbCast.SelectedItem is Beroemdheid beroemdheid)
            {
                cast.Remove(beroemdheid);
                lbCast.Items.Refresh();
            }
        }
    }
}
