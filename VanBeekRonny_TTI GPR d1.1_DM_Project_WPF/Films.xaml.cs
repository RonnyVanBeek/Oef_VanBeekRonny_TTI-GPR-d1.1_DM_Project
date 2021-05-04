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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbFilms.ItemsSource = DatabaseOperations.OphalenFilms();
            cmbTaal.ItemsSource = DatabaseOperations.Talen();
            cmbLeeftijdsgroep.ItemsSource = DatabaseOperations.Leeftijdsgroepen();
        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dataFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbFilms.SelectedItem is Film film)
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
            lbFilms.ItemsSource = DatabaseOperations.OphalenFilms();
        }

        private void btnBijwerken_Click(object sender, RoutedEventArgs e)
        {
            if (lbFilms.SelectedItem is Film film)
            {
                FilmsBewerken filmsBewerken = new FilmsBewerken(film.id);
                filmsBewerken.Title = $"Film '{filmsBewerken.txtTitel.Text}' - aanpassen";
                filmsBewerken.txtTitel.IsEnabled = false;
                filmsBewerken.ShowDialog();
                lbFilms.SelectedIndex = -1;
                lbFilms.ItemsSource = DatabaseOperations.OphalenFilms();
            }
            else
            {
                MessageBox.Show("Er is geen film geselecteerd om te bewerken!");
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder foutmeldingen = new StringBuilder();
            foutmeldingen.Append(Valideer("lbFilms"));
            Film film = lbFilms.SelectedItem as Film;

            if (string.IsNullOrWhiteSpace(foutmeldingen.ToString()))
            {
                if (MessageBox.Show($"Weet u zeker dat u '{film.titel}' wilt verwijderen?","Bevestiging",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"Code uitvoeren om de film {film.titel} te verwijderen.");
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
            if (columnName == "lbFilms" && lbFilms.SelectedItem == null)
            {
                return "Selecteer een film!" + Environment.NewLine;
            }
            return "";
        }

        private void txtZoeken_KeyUp(object sender, KeyEventArgs e)
        {
            lbFilms.ItemsSource = DatabaseOperations.ZoekenFilms(txtZoeken.Text);
            lbFilms.Items.Refresh();
        }

    }
}
