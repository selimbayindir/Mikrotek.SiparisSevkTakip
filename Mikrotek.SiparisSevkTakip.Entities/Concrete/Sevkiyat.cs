using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotek.SiparisSevkTakip.Entities.Concrete
{
    public class Sevkiyat
    {
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public string IrsaliyeNo { get; set; } = null!;
        public string Plaka { get; set; } = null!;
        public decimal MiktarM3 { get; set; }
        public DateTime CikisZamani { get; set; } = DateTime.Now;
    }
}
