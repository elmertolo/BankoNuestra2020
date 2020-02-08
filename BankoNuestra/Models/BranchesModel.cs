using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoNuestra.Models
{
    class BranchesModel
    {
        public string BRSTN { get; set; }
        public DateTime Date { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Address6 { get; set; }
        public Int64 LastNo_P { get; set; }
        public Int64 LastNo_C { get; set; }
        public Int64 LastNo_CS { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
