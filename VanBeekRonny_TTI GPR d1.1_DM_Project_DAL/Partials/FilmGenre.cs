using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    public partial class FilmGenre
    {
        public override bool Equals(object obj)
        {
            return obj is FilmGenre genre &&
                   filmId == genre.filmId &&
                   genreId == genre.genreId;
        }

        public override int GetHashCode()
        {
            int hashCode = 535254147;
            hashCode = hashCode * -1521134295 + filmId.GetHashCode();
            hashCode = hashCode * -1521134295 + genreId.GetHashCode();
            return hashCode;
        }
    }
}
