using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanBeekRonny_TTI_GPR_d1._1_DM_Project_Models;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    public partial class Film : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(titel) && (string.IsNullOrWhiteSpace(titel) || titel.Length>45))
                {
                    return "Titel is een verplicht veld, en moet kleiner zijn dan 45 tekens!";
                }
                if (columnName == nameof(speelduur) &&  speelduur.Length > 10)
                {
                    return "Speelduur moet kleiner zijn dan 10 tekens!";
                }
                if (columnName == nameof(verhaallijn) && verhaallijn.Length > 800)
                {
                    return "Verhaallijn moet kleiner zijn dan 800 tekens!";
                }
                if (columnName == nameof(slogan) && slogan.Length > 250)
                {
                    return "Slogan moet kleiner zijn dan 250 tekens!";
                }
                return "";
            }
        }
    }
}
