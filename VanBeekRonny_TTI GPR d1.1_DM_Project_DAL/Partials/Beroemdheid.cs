using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanBeekRonny_TTI_GPR_d1._1_DM_Project_Models;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    public partial class Beroemdheid:Basisklasse
    {
        public override bool Equals(object obj)
        {
            return obj is Beroemdheid beroemdheid &&
                   naam == beroemdheid.naam &&
                   voornaam == beroemdheid.voornaam &&
                   geboortedatum == beroemdheid.geboortedatum;
        }

        public override int GetHashCode()
        {
            int hashCode = 1641062633;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(naam);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(voornaam);
            hashCode = hashCode * -1521134295 + geboortedatum.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{this.voornaam} {this.naam}";
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(naam) && (string.IsNullOrWhiteSpace(naam) || naam.Length > 22))
                {
                    return "Naam is een verplicht veld, en moet kleiner zijn dan 22 tekens!";
                }
                if (columnName == nameof(voornaam) && (string.IsNullOrWhiteSpace(voornaam) || voornaam.Length > 22))
                {
                    return "Voornaam is een verplicht veld, en moet kleiner zijn dan 22 tekens!";
                }
                if (columnName == nameof(lengte) && lengte < 0)
                {
                    return "Lengte moet groter zijn dan 0!";
                }
                if (columnName == nameof(handelsmerk) && handelsmerk.Length > 100)
                {
                    return "Handelsmerk moet kleiner zijn dan 100 tekens!";
                }
                return "";
            }
        }

    }
}
