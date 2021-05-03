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
    /// Interaction logic for FilmsBewerken.xaml
    /// </summary>
    public partial class FilmsBewerken : Window
    {
        //Globale variabele
        int filmId = 0;
        List<Beroemdheid> cast = new List<Beroemdheid>();
        List<Genre> genres = new List<Genre>();

        public FilmsBewerken()
        {
            InitializeComponent();
            this.btnOpslaan.Content = "Toevoegen";
        }

        public FilmsBewerken(int FilmId)
        {
            InitializeComponent();
            Film film = DatabaseOperations.OphalenFilmsPerId(FilmId);
            this.filmId = FilmId;

            if (FilmId > 0)
            {
                this.txtTitel.Text = film.titel;
                this.dpPublicatiedatum.SelectedDate = film.publicatiedatum;
                this.cmbTaal.SelectedItem = film.Taal;
                this.txtSpeelduur.Text = film.speelduur;
                this.cmbLeeftijdsgroep.SelectedItem = film.Leeftijdsgroep;
                this.txtSlogan.Text = film.slogan;
                this.txtVerhaallijn.Text = film.verhaallijn;
                this.lbCast.ItemsSource = film.FilmBeroemdheden;
                this.lbGenres.ItemsSource = film.FilmGenres;
                this.btnOpslaan.Content = "Bijwerken";

                foreach (FilmBeroemdheid filmBeroemdheid in film.FilmBeroemdheden)
                {
                    cast.Add(filmBeroemdheid.Beroemdheid);
                }

                foreach (FilmGenre filmGenre in film.FilmGenres)
                {
                    genres.Add(filmGenre.Genre);
                }                
            }
            else
            {
                Film nieuweFilm = new Film();
                
            }

            //this.lblBeroemdheden = lblBeroemdheden;
            //this.cmbBeroemdheden = cmbBeroemdheden;
            //this.btnToevoegen = btnToevoegen;
            //this.btnVerwijderen = btnVerwijderen;
            //this.lblCast = lblCast;
            //this.lbCast = lbCast;
            //this.lblGenre = lblGenre;
            //this.cmbGenres = cmbGenres;
            //this.btnGenreToevoegen = btnGenreToevoegen;
            //this.btnGenreVerwijderen = btnGenreVerwijderen;
            //this.lblGenres = lblGenres;
            //this.lbGenres = lbGenres;
            //this.btnOpslaan = btnOpslaan;
            //this.btnAnnuleren = btnAnnuleren;
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbTaal.ItemsSource = DatabaseOperations.Talen();
            cmbLeeftijdsgroep.ItemsSource = DatabaseOperations.Leeftijdsgroepen();
            cmbBeroemdheden.ItemsSource = DatabaseOperations.OphalenBeroemdheden();
            cmbGenres.ItemsSource = DatabaseOperations.OphalenGenres();
            lbCast.ItemsSource = cast;
            lbGenres.ItemsSource = genres;
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBeroemdheden.SelectedItem is Beroemdheid beroemdheid)
            {
                if (!cast.Contains(beroemdheid))
                {
                    cast.Add(beroemdheid);
                    lbCast.Items.Refresh();
                    cmbBeroemdheden.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show($"'{beroemdheid.voornaam} {beroemdheid.naam}' staat al in de lijst.");
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst de beroemdheid die u wilt toevoegen!");
                this.cmbBeroemdheden.Focus();
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lbCast.SelectedItem is Beroemdheid beroemdheid)
            {
                cast.Remove(beroemdheid);
                lbCast.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Selecteer de beroemdheid die u wilt verwijderen!");
            }
        }

        private void btnGenreToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (cmbGenres.SelectedItem is Genre genre)
            {
                if (!genres.Contains(genre))
                {
                    genres.Add(genre);
                    lbGenres.Items.Refresh();
                    cmbGenres.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show($"Genre '{genre.genre}' staat al in de lijst.");
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst het genre dat u wilt toevoegen!");
                this.cmbGenres.Focus();
            }
        }

        private void btnGenreVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lbGenres.SelectedItem is Genre genre)
            {
                genres.Remove(genre);
                lbGenres.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Selecteer het genre dat u wilt verwijderen!");
            }
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder foutmeldingen = new StringBuilder();
            foutmeldingen.Append(Valideer(cmbTaal.ToString()));
            foutmeldingen.Append(Valideer(cmbLeeftijdsgroep.ToString()));
            
            if (string.IsNullOrWhiteSpace(foutmeldingen.ToString()))
            {
                Taal taal = cmbTaal.SelectedItem as Taal;
                Leeftijdsgroep leeftijdsgroep = cmbLeeftijdsgroep.SelectedItem as Leeftijdsgroep;

                if (filmId > 0)
                {
                    Film film = DatabaseOperations.OphalenFilmsPerId(this.filmId);
                    film.titel = txtTitel.Text;
                    film.publicatiedatum = dpPublicatiedatum.SelectedDate.Value;
                    film.speelduur = txtSpeelduur.Text;
                    film.verhaallijn = txtVerhaallijn.Text;
                    film.Taal = taal;
                    film.slogan = txtSlogan.Text;
                    film.Leeftijdsgroep = leeftijdsgroep;
                    
                    MessageBox.Show($"Film {film.titel} bijwerken");

                }
                else
                {
                    Film film = new Film();
                    film.titel = txtTitel.Text;
                    film.publicatiedatum = dpPublicatiedatum.SelectedDate.Value;
                    film.speelduur = txtSpeelduur.Text;
                    film.verhaallijn = txtVerhaallijn.Text;
                    film.Taal = taal;
                    film.slogan = txtSlogan.Text;
                    film.Leeftijdsgroep = leeftijdsgroep;
                    MessageBox.Show("Film toevoegen");
                }

            }
            else
            {
                MessageBox.Show(foutmeldingen.ToString(), "Foutmeldingen");
            }
            
        }

        private string Valideer(string columnName)
        {
            if (columnName == nameof(cmbTaal) && cmbTaal.SelectedItem == null)
            {
                return "Selecteer een taal!" + Environment.NewLine;
            }
            if (columnName == nameof(cmbLeeftijdsgroep) && cmbLeeftijdsgroep.SelectedItem == null)
            {
                return "Selecteer een leeftijdsgroep!" + Environment.NewLine;
            }
            return "";
        }
    }
}
