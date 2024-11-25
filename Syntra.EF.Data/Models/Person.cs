using Microsoft.VisualBasic;
using Syntra.EF.Data.Base;
using Syntra.EF.Data.Defines;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.EF.Data.Models
{
    public class Person : DbBase
    {
        public Person() { }
        public Person(string name, string firstname, DateTime birthDate)
        {
            LastName = name;
            FirstName = firstname;
            BirthDate = birthDate;
        }
        [NotMapped]
        public Defs.Genders Gender { get; set; } = Defs.Genders.Unknown;
        public int GenderId { get => (int)Gender; set { Gender = (Defs.Genders)value; } }   
        [StringLength(250)]
        public string LastName { get; set; } = "";
        [StringLength(250)]
        public string? MiddleName { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; } = "";
        [StringLength(250)]
        public string? NickName { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
