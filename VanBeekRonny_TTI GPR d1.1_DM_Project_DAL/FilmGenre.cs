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
    using System.Collections.Generic;
    
    public partial class FilmGenre
    {
        public int id { get; set; }
        public int filmId { get; set; }
        public int genreId { get; set; }
    
        public virtual Film Film { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
