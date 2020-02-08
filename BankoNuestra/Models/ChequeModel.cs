using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoNuestra.Models
{
    class ChequeModel
    {
        public string BRSTN { get; set; }
        public string AccountNo { get; set; }
        public string ChequeType { get; set; }
        public string ChequeName { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string  Address1 { get; set; }
        public string  Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Address6 { get; set; }
        public int PcsPerBook { get; set; }
        public int Quantity { get; set; }
        public string StartingSerial { get; set; }
        public string EndingSerial { get; set; }
        public DateTime deliveryDate { get; set; }
        public string BatchFile { get; set; }

    }
}
