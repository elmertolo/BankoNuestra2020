using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoNuestra.Models
{
    class Users
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        private string _nameForArchiving;
        public string NameForArchiving
        {
            get
            {
                return Name.Replace(" ", "_");
            }

            set
            {
                _nameForArchiving = value;
            }
        }
    }
}
