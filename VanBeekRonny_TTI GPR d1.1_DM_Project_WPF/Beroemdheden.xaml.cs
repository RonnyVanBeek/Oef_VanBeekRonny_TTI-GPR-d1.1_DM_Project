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
            InvoerelementenReadOnly();
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
                beroemdhedenBewerken.Title = $"Beroemdheid '{beroemdheid.ToString()}' - aanpassen";
                beroemdhedenBewerken.txtVoornaam.IsEnabled = false;
                beroemdhedenBewerken.txtNaam.IsEnabled = false;
                beroemdhedenBewerken.ShowDialog();
                lbBeroemdheden.SelectedIndex = -1;
                lbBeroemdheden.ItemsSource = DatabaseOperations.OphalenBeroemdheden();
                FormulierResetten();
            }
            else
            {
                MessageBox.Show("Er is geen beroemdheid geselecteerd om te bewerken!");
            }
        }

        //public static string VolledigeNaam(Beroemdheid beroemdheid)
        //{
        //    return $"{beroemdheid.voornaam} {beroemdheid.naam}";
        //}

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
                if (MessageBox.Show($"Weet u zeker dat u '{beroemdheid.ToString()}' wilt verwijderen?", "Bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //MessageBox.Show($"Code uitvoeren om de film {beroemdheid.ToString()} te verwijderen.");
                    if (DatabaseOperations.VerwijderenBeroemdheid(beroemdheid) > 0)
                    {
                        lbBeroemdheden.ItemsSource = DatabaseOperations.OphalenBeroemdheden();
                    }
                    else
                    {
                        MessageBox.Show($"Beroemdheid: {beroemdheid} is niet verwijderd.");
                        FormulierResetten();
                    }
                }
                else
                {
                    MessageBox.Show($"'{beroemdheid.ToString()}' is niet verwijderd? Handeling is geannuleerd.", "Verwijderen geannuleerd");
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

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            BeroemdhedenBewerken beroemdhedenBewerken = new BeroemdhedenBewerken();
            beroemdhedenBewerken.Title = $"Beroemdheid - toevoegen";
            beroemdhedenBewerken.txtNaam.Focus();
            beroemdhedenBewerken.ShowDialog();
            lbBeroemdheden.ItemsSource = DatabaseOperations.OphalenBeroemdheden();
            FormulierResetten();
        }

        private void FormulierResetten()
        {
            txtVoornaam.Clear();
            txtAchternaam.Clear();
            dpGeboortedatum.SelectedDate = null;
            cmbNationaliteit.SelectedIndex = -1;
            txtLengte.Clear();
            txtHandelsmerk.Clear();
            cmbSterrenbeeld.SelectedIndex = -1;
        }

        private void InvoerelementenReadOnly()
        {
            txtVoornaam.IsReadOnly = true;
            txtAchternaam.IsReadOnly = true;
            dpGeboortedatum.IsEnabled = false;
            cmbNationaliteit.IsReadOnly = true;
            txtLengte.IsReadOnly = true;
            txtHandelsmerk.IsReadOnly = true;
            cmbSterrenbeeld.IsReadOnly = true;
        }
    }
}
