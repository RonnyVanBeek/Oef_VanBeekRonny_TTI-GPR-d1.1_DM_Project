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

        public FilmsBewerken()
        {
            InitializeComponent();
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

                //filmId = FilmId;

                ////DateTime dpPublicatiedatum = film.publicatiedatum;
                ////Taal cmbTaal = film.Taal;
                //String txtSpeelduur = film.speelduur;
                //Leeftijdsgroep cmbLeeftijdsgroep = film.Leeftijdsgroep;
                //string txtSlogan = film.slogan;
                //string txtVerhaallijn = film.verhaallijn;
                //Beroemdheid cmbBeroemdheden = film.FilmBeroemdheden.;

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
        }
    }
}
