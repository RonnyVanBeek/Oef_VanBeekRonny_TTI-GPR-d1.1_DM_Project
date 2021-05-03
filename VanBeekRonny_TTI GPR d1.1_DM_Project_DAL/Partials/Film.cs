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
                if (columnName == nameof(titel) && string.IsNullOrWhiteSpace(titel))
                {
                    return "Titel is een verplicht veld!";
                }
                return "";
            }
        }
    }
}
