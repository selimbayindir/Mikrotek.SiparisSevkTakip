using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotek.SiparisSevkTakip.Entities.Concrete
{
    public class Siparis
    {
        public int Id { get; set; }
        public int SantiyeId { get; set; }
        public int BetonSinifiId { get; set; }
        public decimal MiktarM3 { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public string Durum { get; set; } = "Beklemede";
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public Santiye? Santiye { get; set; }
        public BetonSinifi? BetonSinifi { get; set; }
        public List<Sevkiyat> Sevkiyatlar { get; set; } = new();
    }
}
