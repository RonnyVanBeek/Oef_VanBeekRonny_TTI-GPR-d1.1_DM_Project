using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VanBeekRonny_TTI_GPR_d1._1_DM_Project_Models;
using VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL;

namespace IMDb_UnitTests
{
    [TestClass]
    public class IMDbFilm
    {
        [TestMethod]
        public void Titel_OphalenNaamViaId_NaamEqualsValue()
        {
            //Arrange >> initialiseren van object
            Film film = DatabaseOperations.OphalenFilmsPerId(1002);

            //Act >> het oproepen van de methode dat getest moet worden
            string filmtitel = "Inception";

            //Assert >> De controle op gewenste waarde
            Assert.AreEqual(filmtitel, film.titel);
        }

        [TestMethod]
        public void Film_IsGeldig_IsGeldigIsFalse()
        {
            //Arrange >> initialiseren van object
            Film film = new Film();
            film.titel = new string('x', 45); //Titel is verplicht in te vullen en mag max. 45 tekens bevatten.
            film.speelduur = new string('x', 10); //Speelduur mag max 10 tekens bevatten.
            film.verhaallijn = new string('x', 800); //Verhaallijn mag max 800 tekens bevatten.
            film.slogan = new string('x', 250); //Slogan mag max 250 tekens bevatten.


            //Assert >> De controle op gewenste waarde
            Assert.IsTrue(film.IsGeldig());
        }
    }
}
