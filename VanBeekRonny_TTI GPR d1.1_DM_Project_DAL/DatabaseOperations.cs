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
                    .Include(x => x.Leeftijdsgroep)
                    .Include(x => x.FilmBeroemdheden);
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
                    //entities.Film.Include(x => x.FilmBeroemdheden);                    
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
                    .Where(x => x.naam.Contains(zoekwaarde) || x.voornaam.Contains(zoekwaarde));
                return query.ToList();
            }
        }

        public static int BeroemdheidToevoegen(Beroemdheid beroemdheid) //5/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {
                    entities.Beroemdheid.Add(beroemdheid);
                    return entities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int BeroemdheidBijwerken(Beroemdheid beroemdheid) //5/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {
                    entities.Entry(beroemdheid).State = EntityState.Modified;
                    return entities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int VerwijderenBeroemdheid(Beroemdheid beroemdheid) //5/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {

                    entities.Entry(beroemdheid).State = EntityState.Deleted;
                    return entities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int ToevoegenFilmBeroemdheid(FilmBeroemdheid filmBeroemdheid) //17/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {

                    entities.FilmBeroemdheid.Add(filmBeroemdheid);
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int ToevoegenFilmGenre(FilmGenre filmGenre) //17/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {

                    entities.FilmGenre.Add(filmGenre);
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static FilmBeroemdheid OphalenFilmBeroemdheidFidBid(int FilmId, int BeroemdheidId) //17/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.FilmBeroemdheid
                    .Where(x => x.filmId.Equals(FilmId) && x.beroemdheidId.Equals(BeroemdheidId));
                return query.SingleOrDefault();
            }
        }

        public static FilmGenre OphalenFilmBeroemdheidFidGid(int FilmId, int GenreId) //17/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.FilmGenre
                    .Where(x => x.filmId.Equals(FilmId) && x.genreId.Equals(GenreId));
                return query.SingleOrDefault();
            }
        }

        public static List<FilmBeroemdheid> OphalenFilmBeroemdhedenPerFilm(int FilmId) //17/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.FilmBeroemdheid
                    .Where(x => x.filmId.Equals(FilmId));
                return query.ToList();
            }
        }

        public static List<FilmGenre> OphalenFilmGenresPerFilm(int FilmId) //17/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.FilmGenre
                    .Where(x => x.filmId.Equals(FilmId));
                return query.ToList();
            }
        }

        public static int VerwijderenFilmBeroemdheid(FilmBeroemdheid filmBeroemdheid) //17/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {
                    entities.Entry(filmBeroemdheid).State = EntityState.Deleted;
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int VerwijderenFilmGenre(FilmGenre filmGenre) //17/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {
                    entities.Entry(filmGenre).State = EntityState.Deleted;
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int ToevoegenGenre(Genre genre) //17/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {

                    entities.Genre.Add(genre);
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int BijwerkenGenre(Genre genre) //17/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {
                    entities.Entry(genre).State = EntityState.Modified;
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int VerwijderenGenre(Genre genre) //17/05/2021
        {
            try
            {
                using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
                {
                    entities.Entry(genre).State = EntityState.Deleted;
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static Genre OphalenGeselecteerdGenre(int genreId) //18/05/2021
        {
            using (IMDbFilmsEntities entities = new IMDbFilmsEntities())
            {
                var query = entities.Genre
                    .Where(x => x.id.Equals(genreId));
                return query.SingleOrDefault();
            }
        }
    }
}
