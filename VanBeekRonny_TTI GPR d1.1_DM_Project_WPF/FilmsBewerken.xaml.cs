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
using VanBeekRonny_TTI_GPR_d1._1_DM_Project_Models;
using VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_WPF
{
    /// <summary>
    /// Interaction logic for FilmsBewerken.xaml
    /// </summary>
    public partial class FilmsBewerken : Window
    {
        //Globale variabele
        int filmId=0;
        Film film = null;
        List<Beroemdheid> cast = new List<Beroemdheid>();
        List<Genre> genres = new List<Genre>();

        public FilmsBewerken()
        {
            InitializeComponent();
            film = new Film();
            this.btnOpslaan.Content = "Toevoegen";
        }

        public FilmsBewerken(int FilmId)
        {
            InitializeComponent();
            if (FilmId > 0)
            {
                film = DatabaseOperations.OphalenFilmsPerId(FilmId);
                this.filmId = FilmId;
                this.txtTitel.Text = film.titel;
                this.dpPublicatiedatum.SelectedDate = film.publicatiedatum;
                this.cmbTaal.SelectedItem = film.Taal;
                this.txtSpeelduur.Text = film.speelduur;
                this.cmbLeeftijdsgroep.SelectedItem = film.Leeftijdsgroep;
                this.txtSlogan.Text = film.slogan;
                this.txtVerhaallijn.Text = film.verhaallijn;

                foreach (FilmBeroemdheid filmBeroemdheid in film.FilmBeroemdheden)
                {
                    cast.Add(filmBeroemdheid.Beroemdheid);
                }

                foreach (FilmGenre filmGenre in film.FilmGenres)
                {
                    genres.Add(filmGenre.Genre);
                }

                this.lbCast.ItemsSource = cast;
                this.lbGenres.ItemsSource = genres;
                this.btnOpslaan.Content = "Bijwerken";

            }
            else
            {
                film = new Film();                
            }

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
                    if (filmId > 0)
                    {
                        FilmBeroemdheid fb = new FilmBeroemdheid();
                        fb.filmId = filmId;
                        fb.beroemdheidId = beroemdheid.id;
                        fb.functie = "Acteur";
                        DatabaseOperations.ToevoegenFilmBeroemdheid(fb);
                    }
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
                if (filmId > 0)
                {
                    List<FilmBeroemdheid> beroemdhedenFilm = DatabaseOperations.OphalenFilmBeroemdhedenPerFilm(filmId);
                    FilmBeroemdheid fb = DatabaseOperations.OphalenFilmBeroemdheidFidBid(filmId, beroemdheid.id);
                    if (beroemdhedenFilm.Contains(fb))
                    {
                        DatabaseOperations.VerwijderenFilmBeroemdheid(fb);
                    }
                }
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
                    if (filmId > 0)
                    {
                        FilmGenre filmGenre = new FilmGenre();
                        filmGenre.filmId = filmId;
                        filmGenre.genreId = genre.id;
                        DatabaseOperations.ToevoegenFilmGenre(filmGenre);
                    }
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
                if (filmId > 0)
                {
                    List<FilmGenre> genresFilm = DatabaseOperations.OphalenFilmGenresPerFilm(filmId);
                    FilmGenre fg = DatabaseOperations.OphalenFilmBeroemdheidFidGid(filmId, genre.id);
                    if (genresFilm.Contains(fg))
                    {
                        DatabaseOperations.VerwijderenFilmGenre(fg);
                    }
                }
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

                
                //film.id = filmId;
                film.titel = txtTitel.Text;
                film.publicatiedatum = dpPublicatiedatum.SelectedDate.Value;
                film.speelduur = txtSpeelduur.Text;
                film.verhaallijn = txtVerhaallijn.Text;
                film.taalId = taal.id;                
                film.slogan = txtSlogan.Text;
                film.leeftijdsgroepId = leeftijdsgroep.id;
                
                if (filmId > 0)
                {
                    film.Taal = taal;
                    film.Leeftijdsgroep = leeftijdsgroep;
                }

                if (film.IsGeldig()) //Controle of de velden die ingevuld moeten zijn, ook ingevuld zijn.
                {
                    int resultaat;
                    string actie = ""; //Actie in messagebox

                    if (filmId > 0) //bewerken
                    {
                        actie = "Bijwerken";
                        resultaat = DatabaseOperations.FilmBijwerken(film); //Query film bijwerken wordt uitgevoerd, en geeft een resultaat terug.
                    }
                    else //nieuw
                    {
                        actie = "Toevoegen";
                        resultaat = DatabaseOperations.FilmToevoegen(film); //Query film toevoegen wordt uitgevoerd, en geeft een resultaat terug.
                    }
                    //Resultaat groter dan 0 > actie is geslaagd, resultaat = 0 > actie is niet geslaagd.
                    if (resultaat > 0)
                    {
                        this.Close(); //Als de film is bijgewerkt of toegevoegd, wordt het venster gesloten.
                    }
                    else
                    {
                        MessageBox.Show($"{actie} van '{film.titel}' is niet gelukt!"); //Als de film niet is bijgewerkt, krijg je deze melding.
                    }
                }
                else
                {
                    MessageBox.Show(film.Error, "Foutmelding");
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
