using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    public static class DatabaseOperations
    {
        public static List<Film> OphalenFilms()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Film
                    .Include(x => x.Taal)
                    .Include(x => x.Leeftijdsgroep);
                return query.ToList();
            }
        }

        public static List<Beroemdheid> OphalenBeroemdheden()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Beroemdheid
                    .Include(x => x.Nationaliteit)
                    .OrderBy(x => x.naam)
                    .ThenBy(x => x.voornaam);
                return query.ToList();
            }
        }

        public static List<Taal> Talen()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Taal;
                return query.ToList();
            }
        }

        public static List<Leeftijdsgroep> Leeftijdsgroepen()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Leeftijdsgroep;
                return query.ToList();
            }
        }

        public static List<Nationaliteit> Nationaliteiten()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Nationaliteit;
                return query.ToList();
            }
        }

        public static List<Sterrenbeeld> Sterrenbeelden()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Sterrenbeeld;
                return query.ToList();
            }
        }
    }
}
