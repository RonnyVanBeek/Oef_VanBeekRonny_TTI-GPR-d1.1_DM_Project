using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    public partial class FilmBeroemdheid
    {
        public override bool Equals(object obj)
        {
            return obj is FilmBeroemdheid beroemdheid &&
                   filmId == beroemdheid.filmId &&
                   beroemdheidId == beroemdheid.beroemdheidId;
        }

        public override int GetHashCode()
        {
            int hashCode = 699551146;
            hashCode = hashCode * -1521134295 + filmId.GetHashCode();
            hashCode = hashCode * -1521134295 + beroemdheidId.GetHashCode();
            return hashCode;
        }
    }
}
