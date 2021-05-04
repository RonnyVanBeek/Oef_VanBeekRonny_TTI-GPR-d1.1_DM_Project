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
    /// Interaction logic for BeroemdhedenBewerken.xaml
    /// </summary>
    public partial class BeroemdhedenBewerken : Window
    {
        int beroemdheidId = 0;

        public BeroemdhedenBewerken()
        {
            InitializeComponent();
        }

        public BeroemdhedenBewerken(int beroemdheidId)
        {
            InitializeComponent();
            Beroemdheid beroemdheid = DatabaseOperations.OphalenBeroemdheidPerId(beroemdheidId);            
            this.beroemdheidId = beroemdheid.id;            
            this.txtNaam.Text = beroemdheid.naam;
            this.txtVoornaam.Text = beroemdheid.voornaam;
            this.dpGeboortedatum.SelectedDate = beroemdheid.geboortedatum;
            this.cmbNationaliteit.SelectedItem = beroemdheid.Nationaliteit;
            this.txtLengte.Text = beroemdheid.lengte;
            this.cmbSterrenbeeld.SelectedItem = beroemdheid.Sterrenbeeld;
            this.txtHandelsmerk.Text = beroemdheid.handelsmerk;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbNationaliteit.ItemsSource = DatabaseOperations.Nationaliteiten();
            cmbSterrenbeeld.ItemsSource = DatabaseOperations.Sterrenbeelden();
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
