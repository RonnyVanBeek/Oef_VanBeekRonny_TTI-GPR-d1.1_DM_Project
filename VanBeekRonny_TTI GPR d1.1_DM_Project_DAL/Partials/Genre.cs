using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanBeekRonny_TTI_GPR_d1._1_DM_Project_Models;

namespace VanBeekRonny_TTI_GPR_d1._1_DM_Project_DAL
{
    public partial class Genre:Basisklasse
    {
        //public override bool Equals(object obj)
        //{
        //    return obj is Genre genre &&
        //           id == genre.id;
        //}

        //public override int GetHashCode()
        //{
        //    return 1877310944 + id.GetHashCode();
        //}



        public override string ToString()
        {
            return this.genre;
        }

        public override bool Equals(object obj)
        {
            return obj is Genre genre &&
                   this.genre == genre.genre;
        }

        public override int GetHashCode()
        {
            return 680280000 + EqualityComparer<string>.Default.GetHashCode(genre);
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(genre) && (string.IsNullOrWhiteSpace(genre) || genre.Length > 10))
                {
                    return "Genre is een verplicht veld, en moet kleiner zijn dan 10 tekens!";
                }
                return "";
            }
        }


    }
}
