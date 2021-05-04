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

        public static Film OphalenFilmsPerId(int FilmId) //3/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Film
                    .Include(x => x.Taal)
                    .Include(x => x.Leeftijdsgroep)
                    .Include(x => x.FilmGenres.Select(y => y.Genre))
                    .Include(x => x.FilmBeroemdheden.Select(y => y.Beroemdheid))
                    .Where(x => x.id.Equals(FilmId));
                return query.SingleOrDefault();
            }
        }

        public static List<Beroemdheid> OphalenBeroemdheden()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Beroemdheid
                    .Include(x => x.Nationaliteit)
                    .Include(x => x.Sterrenbeeld)
                    .OrderBy(x => x.naam)
                    .ThenBy(x => x.voornaam);
                return query.ToList();
            }
        }

        public static List<Genre> OphalenGenres()
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Genre
                    .Include(x => x.FilmGenres)
                    .OrderBy(x => x.genre);
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

        public static int VerwijderenFilm(Film film) //3/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {

                    entities.Entry(film).State = EntityState.Deleted;
                    return entities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static List<Film> ZoekenFilms(string zoekwaarde) //3/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Film
                    .Include(x => x.Taal)
                    .Include(x => x.Leeftijdsgroep)
                    .Where(x => x.titel.Contains(zoekwaarde));
                return query.ToList();
            }
        }

        public static int FilmToevoegen(Film film) //4/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {
                    entities.Film.Add(film);
                    return entities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int FilmBijwerken(Film film) //4/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {
                    entities.Entry(film).State = EntityState.Modified;
                    return entities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static Beroemdheid OphalenBeroemdheidPerId(int BeroemdheidId) //4/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Beroemdheid
                    .Include(x => x.Nationaliteit)
                    .Include(x => x.Sterrenbeeld)
                    .Where(x => x.id.Equals(BeroemdheidId));
                return query.SingleOrDefault();
            }
        }
        public static List<Beroemdheid> ZoekenBeroemdheden(string zoekwaarde) //4/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Beroemdheid
                    .Include(x => x.Nationaliteit)
                    .Include(x => x.Sterrenbeeld)
                    .Where(x => x.naam.Contains(zoekwaarde));
                return query.ToList();
            }
        }
    }
}
