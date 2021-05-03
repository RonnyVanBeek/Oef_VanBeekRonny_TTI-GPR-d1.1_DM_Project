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
            FilmsNieuw filmsNieuw = new FilmsNieuw();
            filmsNieuw.ShowDialog();
        }

        private void btnBijwerken_Click(object sender, RoutedEventArgs e)
        {
            if (lbFilms.SelectedItem is Film film)
            {
                FilmsBewerken filmsBewerken = new FilmsBewerken(film.id);
                filmsBewerken.ShowDialog();
            }
        }
    }
}
