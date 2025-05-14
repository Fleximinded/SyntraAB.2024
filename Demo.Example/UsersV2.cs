using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Example.Version2
{
    [PersistableClass("Gebruikers")]
    public class Users : IUsers, ICloneable
    {
        public object Clone()
        {
            return new Users(_count, Id)
            {
                Name = Name,
                Description = Description,
                Email = Email,
                Phone = Phone,
                Password = Password
            };
        }
        public static string DefaultName = "Default";
        protected int _count = 0;
        public Users()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Users(int num, string? id = null)
        {
            _count = num;
            Id = id ?? Guid.NewGuid().ToString();
        }

        public string Id { get; set; } = string.Empty;
        [CustomField("Naam")]
        public string Name { get; set; } = DefaultName;
        [CustomField("Omschrijving", visible: false, persist: true)]
        public string Description { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [CustomField("Telefoon", visible: true, persist: false)]
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Login(string password)
        {
            return password?.Length > 8 && password == Password;
        }
    }
}
