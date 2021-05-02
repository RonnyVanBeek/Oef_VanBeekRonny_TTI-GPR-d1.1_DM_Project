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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnFilm_Click(object sender, RoutedEventArgs e)
        {
            Films films = new Films();
            films.ShowDialog();
        }

        private void btnAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBeroemdheden_Click(object sender, RoutedEventArgs e)
        {
            Beroemdheden beroemdheden = new Beroemdheden();
            beroemdheden.ShowDialog();
        }

        private void KnoppenIngeschakeld(bool status)
        {
            btnGenres.IsEnabled = status;
            btnTalen.IsEnabled = status;
            btnLeeftijdsgroepen.IsEnabled = status;
            btnNationaliteiten.IsEnabled = status;
            btnSterrenbeelden.IsEnabled = status;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KnoppenIngeschakeld(false);
        }
    }
}
