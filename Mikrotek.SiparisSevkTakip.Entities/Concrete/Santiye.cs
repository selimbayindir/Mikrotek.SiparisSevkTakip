using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotek.SiparisSevkTakip.Entities.Concrete
{
    public class Santiye
    {
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public string Ad { get; set; } = null!;
        public string Adres { get; set; } = null!;
        public bool Aktif { get; set; } = true;
    }
}
