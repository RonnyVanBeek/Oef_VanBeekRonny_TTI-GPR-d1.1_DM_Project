//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IMDbFilmsEntities : DbContext
    {
        public IMDbFilmsEntities()
            : base("name=IMDbFilmsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<FilmBeroemdheid> FilmBeroemdheid { get; set; }
        public virtual DbSet<FilmGenre> FilmGenre { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Leeftijdsgroep> Leeftijdsgroep { get; set; }
        public virtual DbSet<Taal> Taal { get; set; }
        public virtual DbSet<Beroemdheid> Beroemdheid { get; set; }
        public virtual DbSet<Nationaliteit> Nationaliteit { get; set; }
        public virtual DbSet<Sterrenbeeld> Sterrenbeeld { get; set; }
    }
}
