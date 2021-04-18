using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    public static class DatabaseOperations
    {
        public static List<Film> OphalenFilms()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Film;
                return query.ToList();
            }
        }
    }
}
