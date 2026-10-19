using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotek.SiparisSevkTakip.Entities.DTOs
{
    public class SevkiyatEkleModel
    {
        [Required(ErrorMessage = "İrsaliye numarası zorunludur.")]
        public string IrsaliyeNo { get; set; } = null!;

        [Required(ErrorMessage = "Plaka zorunludur.")]
        public string Plaka { get; set; } = null!;

        // İş Kuralı 3: Tek bir sevkiyat en fazla 12 m3 olabilir ve pozitif olmalıdır.
        [Range(0.1, 12.0, ErrorMessage = "Sevkiyat miktarı 0'dan büyük ve en fazla 12 m³ olabilir.")]
        public decimal MiktarM3 { get; set; }
    }
}
