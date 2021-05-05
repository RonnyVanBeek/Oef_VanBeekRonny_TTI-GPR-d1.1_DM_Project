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
    /// Interaction logic for BeroemdhedenBewerken.xaml
    /// </summary>
    public partial class BeroemdhedenBewerken : Window
    {
        int beroemdheidId = 0;

        public BeroemdhedenBewerken()
        {
            InitializeComponent();
            this.btnOpslaan.Content = "Toevoegen";
        }

        public BeroemdhedenBewerken(int beroemdheidId)
        {
            InitializeComponent();
            Beroemdheid beroemdheid = DatabaseOperations.OphalenBeroemdheidPerId(beroemdheidId);            
            this.beroemdheidId = beroemdheid.id;            
            this.txtNaam.Text = beroemdheid.naam;
            this.txtVoornaam.Text = beroemdheid.voornaam;
            this.dpGeboortedatum.SelectedDate = beroemdheid.geboortedatum;
            this.cmbNationaliteit.SelectedItem = beroemdheid.Nationaliteit;
            this.txtLengte.Text = beroemdheid.lengte.ToString();
            this.cmbSterrenbeeld.SelectedItem = beroemdheid.Sterrenbeeld;
            this.txtHandelsmerk.Text = beroemdheid.handelsmerk;
            this.btnOpslaan.Content = "Bijwerken";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbNationaliteit.ItemsSource = DatabaseOperations.Nationaliteiten();
            cmbSterrenbeeld.ItemsSource = DatabaseOperations.Sterrenbeelden();
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder foutmeldingen = new StringBuilder();
            foutmeldingen.Append(Valideer("cmbNationaliteit"));
            foutmeldingen.Append(Valideer("cmbSterrenbeeld"));
            foutmeldingen.Append(Valideer("txtLengte"));

            if (string.IsNullOrWhiteSpace(foutmeldingen.ToString()))
            {                
                Nationaliteit nationaliteit = cmbNationaliteit.SelectedItem as Nationaliteit;
                Sterrenbeeld sterrenbeeld = cmbSterrenbeeld.SelectedItem as Sterrenbeeld;


                //Nieuw object aanmaken om binnen de IF ofwel te gaan bijwerken, ofwel te gaan toevoegen.
                Beroemdheid beroemdheid = new Beroemdheid();
                beroemdheid.naam = txtNaam.Text;
                beroemdheid.voornaam = txtVoornaam.Text;
                beroemdheid.geboortedatum = dpGeboortedatum.SelectedDate;
                beroemdheid.nationaliteitId = nationaliteit.id;
                beroemdheid.lengte = double.Parse(txtLengte.Text);
                beroemdheid.sterrenbeeldId = sterrenbeeld.id;
                beroemdheid.handelsmerk = txtHandelsmerk.Text;

                if (beroemdheid.IsGeldig())//Controle of alle vereiste velden zijn ingevuld.
                {
                    int resultaat;
                    string actie = ""; //Actie in messagebox

                    //Controle of dit formulier gebruikt wordt om een nieuwe aan te maken of te bewerken.
                    if (beroemdheidId > 0) //bewerken
                    {
                        actie = "Bijwerken";
                        beroemdheid.id = beroemdheidId;
                        resultaat = DatabaseOperations.BeroemdheidBijwerken(beroemdheid);
                    }
                    else //nieuwe
                    {
                        actie = "Toevoegen";
                        resultaat = DatabaseOperations.BeroemdheidToevoegen(beroemdheid);
                    }
                    //Resultaat groter dan 0 > actie is geslaagd, resultaat = 0 > actie is niet geslaagd.
                    if (resultaat > 0)
                    {
                        MessageBox.Show($"{actie} van {beroemdheid.ToString()} gelukt");
                        this.Close(); //Als de beroemdheid is bijgewerkt of toegevoegd, wordt het venster gesloten.
                    }
                    else
                    {
                        MessageBox.Show($"{actie} van '{beroemdheid.ToString()}' is niet gelukt!"); //Als de film niet is bijgewerkt, krijg je deze melding.
                    }
                }
                else //object is niet geldig
                {
                    MessageBox.Show(beroemdheid.Error, "Foutmelding");
                }
                
            }
            else
            {
                MessageBox.Show(foutmeldingen.ToString(), "Foutmelding");
            }
        }

        private string Valideer(string columnName)
        {
            if (columnName == nameof(cmbNationaliteit) && cmbNationaliteit.SelectedItem == null)
            {
                return "Selecteer een nationaliteit!" + Environment.NewLine;
            }
            if (columnName == nameof(cmbSterrenbeeld) && cmbSterrenbeeld.SelectedItem == null)
            {
                return "Selecteer een sterrenbeeld!" + Environment.NewLine;
            }
            if (columnName == nameof(txtLengte) && double.TryParse(txtLengte.Text,out double length)==false)
            {
                return "Lengte moet een numerieke waarde bevatten!!" + Environment.NewLine;
            }
            return "";
        }
    }
}
