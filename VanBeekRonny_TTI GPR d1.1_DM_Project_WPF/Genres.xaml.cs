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
    /// Interaction logic for Genres.xaml
    /// </summary>
    public partial class Genres : Window
    {
        
        //Globale variabelen
        List<Genre> lijstGenres = new List<Genre>();
        //Genre genre = null;

        public Genres()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lijstGenres = DatabaseOperations.OphalenGenres();
            dgGenre.ItemsSource = lijstGenres;
        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder foutmeldingen = new StringBuilder();
            foutmeldingen.Append(Valideer(dgGenre.ToString()));

            if (string.IsNullOrWhiteSpace(foutmeldingen.ToString()))
            {
                if (dgGenre.SelectedItem is Genre genre)
                {
                    lijstGenres.Remove(genre);
                    DatabaseOperations.VerwijderenGenre(genre);
                    dgGenre.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Selecteer het genre dat u wil verwijderen!");
                    dgGenre.Focus();
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen.ToString(), "Foutmeldingen");
            }
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            Genre genre = new Genre();
            genre.genre = txtGenre.Text;

            if (genre.IsGeldig())
            {
                if (!lijstGenres.Contains(genre))
                {
                    DatabaseOperations.ToevoegenGenre(genre);
                    lijstGenres = DatabaseOperations.OphalenGenres();
                    dgGenre.ItemsSource = lijstGenres;
                }
                else
                {
                    MessageBox.Show($"{genre.genre} bestaat al in de lijst met genre's!");
                }
                txtGenre.Clear();
            }
            else
            {
                MessageBox.Show(genre.Error, "Foutmelding");
                txtGenre.Focus();
            }
        }

        private string Valideer(string columnName)
        {
            if (columnName == nameof(dgGenre) && dgGenre.SelectedItem == null)
            {
                return "Selecteer een genre!" + Environment.NewLine;
            }
            return "";
        }

        private void btnBijwerken_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder foutmeldingen = new StringBuilder();
            foutmeldingen.Append(Valideer(dgGenre.Name));

            if (string.IsNullOrWhiteSpace(foutmeldingen.ToString()))
            {
                Genre genre = dgGenre.SelectedItem as Genre;

                genre.genre = txtGenre.Text;
                if (genre.IsGeldig())
                {
                    int res = DatabaseOperations.BijwerkenGenre(genre);
                    dgGenre.ItemsSource = DatabaseOperations.OphalenGenres();
                    //dgGenre.Items.Refresh();
                    dgGenre.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show(genre.Error, "Foutmelding");
                }
                //genre = null;
            }
            else
            {
                MessageBox.Show(foutmeldingen.ToString(), "Foutmeldingen");
            }
        }

        private void dgGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StringBuilder foutmeldingen = new StringBuilder();
            foutmeldingen.Append(Valideer(dgGenre.ToString()));

            if (string.IsNullOrWhiteSpace(foutmeldingen.ToString()))
            {
                Genre genre = dgGenre.SelectedItem as Genre;
                txtGenre.Text = genre.genre;
            }
        }
    }
}
