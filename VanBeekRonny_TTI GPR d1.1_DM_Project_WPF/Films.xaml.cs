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
    /// Interaction logic for Films.xaml
    /// </summary>
    public partial class Films : Window
    {
        public Films()
        {
            InitializeComponent();
            FormulierResetten();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgFilms.ItemsSource = DatabaseOperations.OphalenFilms();
            cmbTaal.ItemsSource = DatabaseOperations.Talen();
            cmbLeeftijdsgroep.ItemsSource = DatabaseOperations.Leeftijdsgroepen();
            InvoerelementenReadOnly();
        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dataFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFilms.SelectedItem is Film film)
            {
                txtTitel.Text = film.titel;
                dpPublicatiedatum.SelectedDate = film.publicatiedatum;
                txtSpeelduur.Text = film.speelduur;                
                cmbTaal.SelectedItem = film.Taal;
                txtSlogan.Text = film.slogan;
                cmbLeeftijdsgroep.SelectedItem = film.Leeftijdsgroep;
                txtVerhaallijn.Text = film.verhaallijn;
            }
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            //FilmsNieuw filmsNieuw = new FilmsNieuw();
            //filmsNieuw.ShowDialog();
            FilmsBewerken nieuweFilm = new FilmsBewerken();
            nieuweFilm.Title = "Film - toevoegen";
            nieuweFilm.ShowDialog();
            dgFilms.ItemsSource = DatabaseOperations.OphalenFilms();
        }

        private void btnBijwerken_Click(object sender, RoutedEventArgs e)
        {
            if (dgFilms.SelectedItem is Film film)
            {
                FilmsBewerken filmsBewerken = new FilmsBewerken(film.id);
                filmsBewerken.Title = $"Film '{filmsBewerken.txtTitel.Text}' - aanpassen";
                filmsBewerken.txtTitel.IsEnabled = false;
                filmsBewerken.ShowDialog();
                dgFilms.SelectedIndex = -1;
                dgFilms.ItemsSource = DatabaseOperations.OphalenFilms();
                FormulierResetten();
            }
            else
            {
                MessageBox.Show("Er is geen film geselecteerd om te bewerken!");
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder foutmeldingen = new StringBuilder();
            foutmeldingen.Append(Valideer("dgFilms"));
            Film film = dgFilms.SelectedItem as Film;

            if (string.IsNullOrWhiteSpace(foutmeldingen.ToString()))
            {
                if (MessageBox.Show($"Weet u zeker dat u '{film.titel}' wilt verwijderen?","Bevestiging",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //MessageBox.Show($"Code uitvoeren om de film {film.titel} te verwijderen.");
                    if (DatabaseOperations.VerwijderenFilm(film) > 0)
                    {
                        dgFilms.ItemsSource = DatabaseOperations.OphalenFilms();
                    }
                    else
                    {
                        MessageBox.Show($"Film: {film} is niet verwijderd.");
                    }
                    FormulierResetten();
                }
                else
                {
                    MessageBox.Show($"'{film.titel}' is niet verwijderd? Handeling is geannuleerd.","Verwijderen geannuleerd");
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen.ToString(),"Verwijderen",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private string Valideer(string columnName)
        {
            if (columnName == "dgFilms" && dgFilms.SelectedItem == null)
            {
                return "Selecteer een film!" + Environment.NewLine;
            }
            return "";
        }

        private void txtZoeken_KeyUp(object sender, KeyEventArgs e)
        {
            dgFilms.ItemsSource = DatabaseOperations.ZoekenFilms(txtZoeken.Text);
            dgFilms.Items.Refresh();
        }

        private void FormulierResetten()
        {
            txtTitel.Clear();
            dpPublicatiedatum.SelectedDate = null;
            txtSpeelduur.Clear();
            cmbTaal.SelectedIndex = -1;
            txtSlogan.Clear();
            cmbLeeftijdsgroep.SelectedIndex = -1;
            txtVerhaallijn.Clear();
            txtZoeken.Clear();
        }

        private void InvoerelementenReadOnly()
        {
            txtTitel.IsReadOnly = true;
            dpPublicatiedatum.IsEnabled = false;
            txtSpeelduur.IsReadOnly = true;
            cmbTaal.IsReadOnly = true;
            txtSlogan.IsReadOnly = true;
            cmbLeeftijdsgroep.IsReadOnly = true;
            txtVerhaallijn.IsReadOnly = true;
        }
    }
}
