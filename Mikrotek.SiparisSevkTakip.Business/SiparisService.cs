using Microsoft.EntityFrameworkCore;
using Mikrotek.SiparisSevkTakip.DataAccess.Context;
using Mikrotek.SiparisSevkTakip.Entities.Concrete;
using Mikrotek.SiparisSevkTakip.Entities.DTOs;

namespace Mikrotek.SiparisSevkTakip.Business
{
    public class SiparisService
    {
        private readonly AppDbContext _context;

        public SiparisService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Siparis> SiparisOlusturAsync(SiparisOlusturModel model)
        {
            var santiye = await _context.Santiye.FindAsync(model.SantiyeId);
            if (santiye == null || !santiye.Aktif)
                throw new ArgumentException("Geçerli veya aktif bir şantiye bulunamadı.");

            var beton = await _context.BetonSinifi.FindAsync(model.BetonSinifiId);
            if (beton == null)
                throw new ArgumentException("Geçerli bir beton sınıfı bulunamadı.");

            var yeniSiparis = new Siparis
            {
                SantiyeId = model.SantiyeId,
                BetonSinifiId = model.BetonSinifiId,
                MiktarM3 = model.MiktarM3,
                TeslimTarihi = model.TeslimTarihi,
                Durum = "Beklemede",
                OlusturmaTarihi = DateTime.Now
            };

            _context.Siparis.Add(yeniSiparis);
            await _context.SaveChangesAsync();
            return yeniSiparis;
        }
        public async Task<object> SiparisleriGetirAsync(string? durum, int? santiyeId, int sayfa = 1, int boyut = 10)
        {
            var query = _context.Siparis.AsQueryable();

            if (!string.IsNullOrEmpty(durum))
                query = query.Where(s => s.Durum == durum);

            if (santiyeId.HasValue)
                query = query.Where(s => s.SantiyeId == santiyeId.Value);

            var toplamKayit = await query.CountAsync();
            var veriler = await query
                .OrderByDescending(s => s.OlusturmaTarihi)
                .Skip((sayfa - 1) * boyut)
                .Take(boyut)
                .ToListAsync();

            return new { ToplamKayit = toplamKayit, Sayfa = sayfa, Boyut = boyut, Veriler = veriler };
        }
        public async Task<object?> SiparisDetayGetirAsync(int id)
        {
            var siparis = await _context.Siparis
                .Include(s => s.Santiye)
                .Include(s => s.BetonSinifi)
                .Include(s => s.Sevkiyatlar)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (siparis == null) return null;

            var gonderilenM3 = siparis.Sevkiyatlar.Sum(x => x.MiktarM3);
            var kalanM3 = siparis.MiktarM3 - gonderilenM3;
            return new
            {
                siparis.Id,
                SantiyeAdi = siparis.Santiye?.Ad,
                BetonSinifi = siparis.BetonSinifi?.Kod,
                SiparisEdilenM3 = siparis.MiktarM3,
                GonderilenM3 = gonderilenM3,
                KalanM3 = kalanM3,
                siparis.Durum,
                siparis.TeslimTarihi,
                Sevkiyatlar = siparis.Sevkiyatlar
            };
        }
        public async Task<Sevkiyat> SevkiyatEkleAsync(int siparisId, SevkiyatEkleModel model)
        {
            var siparis = await _context.Siparis
                .Include(s => s.Sevkiyatlar)
                .FirstOrDefaultAsync(s => s.Id == siparisId);

            if (siparis == null)
                throw new KeyNotFoundException("Sipariş bulunamadı.");

            if (siparis.Durum == "İptal" || siparis.Durum == "Tamamlandı")
                throw new InvalidOperationException($"{siparis.Durum} durumundaki bir siparişe sevkiyat eklenemez.");

            if (model.MiktarM3 <= 0 || model.MiktarM3 > 12)
                throw new InvalidOperationException("Tek bir sevkiyat 0'dan büyük ve en fazla 12 m³ olabilir.");

            // Kural 7: İrsaliye no benzersiz olmalıdır.
            bool irsaliyeVarMi = await _context.Sevkiyat.AnyAsync(x => x.IrsaliyeNo == model.IrsaliyeNo);
            if (irsaliyeVarMi)
                throw new InvalidOperationException("Bu irsaliye numarası zaten kullanılmış.");

            decimal mevcutGonderilen = siparis.Sevkiyatlar.Sum(x => x.MiktarM3);
            decimal yeniToplam = mevcutGonderilen + model.MiktarM3;

            if (yeniToplam > siparis.MiktarM3)
                throw new InvalidOperationException($"Sevkiyat miktarı kalan sipariş miktarını aşıyor! Kalan miktar: {siparis.MiktarM3 - mevcutGonderilen} m³.");

            var yeniSevkiyat = new Sevkiyat
            {
                SiparisId = siparisId,
                IrsaliyeNo = model.IrsaliyeNo,
                Plaka = model.Plaka,
                MiktarM3 = model.MiktarM3,
                CikisZamani = DateTime.Now
            };

            _context.Sevkiyat.Add(yeniSevkiyat);

            if (yeniToplam == siparis.MiktarM3)
                siparis.Durum = "Tamamlandı";
            else
                siparis.Durum = "Sevkiyatta";

            await _context.SaveChangesAsync();
            return yeniSevkiyat;
        }
        public async Task SiparisIptalEtAsync(int siparisId)
        {
            var siparis = await _context.Siparis
                .Include(s => s.Sevkiyatlar)
                .FirstOrDefaultAsync(s => s.Id == siparisId);

            if (siparis == null)
                throw new KeyNotFoundException("Sipariş bulunamadı.");

            // Kural 6: Sevkiyatı olan sipariş iptal edilemez.
            if (siparis.Sevkiyatlar.Any())
                throw new InvalidOperationException("Sevkiyatı başlamış olan bir sipariş iptal edilemez.");

            if (siparis.Durum == "İptal")
                throw new InvalidOperationException("Sipariş zaten iptal edilmiş.");

            siparis.Durum = "İptal";
            await _context.SaveChangesAsync();
        }
    }
}
