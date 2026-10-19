using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotek.SiparisSevkTakip.Entities.Concrete
{
    public class BetonSinifi
    {
        public int Id { get; set; }
        public string Kod { get; set; } = null!;
        public decimal BirimFiyat { get; set; }
    }   
}
