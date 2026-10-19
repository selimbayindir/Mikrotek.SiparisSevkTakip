using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotek.SiparisSevkTakip.Entities.DTOs
{
    public class SiparisOlusturModel
    {
        [Required(ErrorMessage = "Şantiye seçimi zorunludur.")]
        public int SantiyeId { get; set; }

        [Required(ErrorMessage = "Beton sınıfı seçimi zorunludur.")]
        public int BetonSinifiId { get; set; }

        [Range(0.1, 10000, ErrorMessage = "Sipariş miktarı 0'dan büyük olmalıdır.")]
        public decimal MiktarM3 { get; set; }

        [Required(ErrorMessage = "Teslim tarihi zorunludur.")]
        public DateTime TeslimTarihi { get; set; }
    }
}
