using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    public partial class Leeftijdsgroep
    {
        public override bool Equals(object obj)
        {
            return obj is Leeftijdsgroep leeftijdsgroep &&
                   id == leeftijdsgroep.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public override string ToString()
        {
            return leeftijdsgroep;
        }
    }
}
