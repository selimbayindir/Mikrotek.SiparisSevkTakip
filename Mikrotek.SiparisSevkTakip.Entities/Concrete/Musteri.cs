using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotek.SiparisSevkTakip.Entities.Concrete
{
    public class Musteri
    {
        public int Id { get; set; }
        public string Unvan { get; set; } = null!;
        public string VergiNo { get; set; } = null!;
        public string Telefon { get; set; } = null!;
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
