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
            lbBeroemdheden.ItemsSource = DatabaseOperations.OphalenBeroemdheden();
            cmbNationaliteit.ItemsSource = DatabaseOperations.Nationaliteiten();
            cmbSterrenbeeld.ItemsSource = DatabaseOperations.Sterrenbeelden();
        }

        private void dataBeroemdheden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbBeroemdheden.SelectedItem is Beroemdheid beroemdheid)
            {
                txtVoornaam.Text = beroemdheid.voornaam;
                txtAchternaam.Text = beroemdheid.naam;
                dpGeboortedatum.SelectedDate = beroemdheid.geboortedatum;
                cmbNationaliteit.SelectedItem = beroemdheid.Nationaliteit;
                txtLengte.Text = beroemdheid.lengte;
                txtHandelsmerk.Text = beroemdheid.handelsmerk;
                cmbSterrenbeeld.SelectedItem = beroemdheid.Sterrenbeeld;
            }
        }

        private void btnBijwerken_Click(object sender, RoutedEventArgs e)
        {            
            if (lbBeroemdheden.SelectedItem is Beroemdheid beroemdheid)
            {
                BeroemdhedenBewerken beroemdhedenBewerken = new BeroemdhedenBewerken(beroemdheid.id);
                beroemdhedenBewerken.Title = $"Beroemdheid '{VolledigeNaam(beroemdheid)}' - aanpassen";
                beroemdhedenBewerken.txtVoornaam.IsEnabled = false;
                beroemdhedenBewerken.txtNaam.IsEnabled = false;
                beroemdhedenBewerken.ShowDialog();
                lbBeroemdheden.SelectedIndex = -1;
                lbBeroemdheden.ItemsSource = DatabaseOperations.OphalenBeroemdheden();
            }
            else
            {
                MessageBox.Show("Er is geen beroemdheid geselecteerd om te bewerken!");
            }
        }

        private string VolledigeNaam(Beroemdheid beroemdheid)
        {
            return $"{beroemdheid.voornaam} {beroemdheid.naam}";
        }

        private void txtZoeken_KeyUp(object sender, KeyEventArgs e)
        {
            lbBeroemdheden.ItemsSource = DatabaseOperations.ZoekenBeroemdheden(txtZoeken.Text);
            lbBeroemdheden.Items.Refresh();
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder foutmeldingen = new StringBuilder();
            foutmeldingen.Append(Valideer("lbBeroemdheden"));
            Beroemdheid beroemdheid = lbBeroemdheden.SelectedItem as Beroemdheid;

            if (string.IsNullOrWhiteSpace(foutmeldingen.ToString()))
            {
                if (MessageBox.Show($"Weet u zeker dat u '{VolledigeNaam(beroemdheid)}' wilt verwijderen?", "Bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"Code uitvoeren om de film {VolledigeNaam(beroemdheid)} te verwijderen.");
                    //if (DatabaseOperations.VerwijderenFilm(film)>0)
                    //{
                    //    lbFilms.ItemsSource = DatabaseOperations.OphalenFilms();
                    //}
                    //else
                    //{
                    //    MessageBox.Show($"Film: {film} is niet verwijderd.");
                    //}
                }
                else
                {
                    MessageBox.Show($"'{VolledigeNaam(beroemdheid)}' is niet verwijderd? Handeling is geannuleerd.", "Verwijderen geannuleerd");
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen.ToString(), "Verwijderen", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private string Valideer(string columnName)
        {
            if (columnName == "lbBeroemdheden" && lbBeroemdheden.SelectedItem == null)
            {
                return "Selecteer een beroemdheid!" + Environment.NewLine;
            }
            return "";
        }
    }
}
