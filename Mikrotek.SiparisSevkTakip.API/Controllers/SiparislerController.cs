using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mikrotek.SiparisSevkTakip.Business;
using Mikrotek.SiparisSevkTakip.Entities.DTOs;

namespace Mikrotek.SiparisSevkTakip.API.Controllers
{
    [ApiController]
    [Route("api/siparisler")]
    public class SiparislerController : ControllerBase
    {
        private readonly SiparisService _siparisService;

        public SiparislerController(SiparisService siparisService)
        {
            _siparisService = siparisService;
        }

  
        [HttpPost]
        public async Task<IActionResult> SiparisOlustur([FromBody] SiparisOlusturModel model)
        {
            try
            {
                var sonuc = await _siparisService.SiparisOlusturAsync(model);
                return CreatedAtAction(nameof(SiparisDetay), new { id = sonuc.Id }, sonuc);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Hata = ex.Message });
            }
        }

 
        [HttpGet]
        public async Task<IActionResult> SiparisListesi(
            [FromQuery] string? durum,
            [FromQuery] int? santiyeId,
            [FromQuery] int sayfa = 1,
            [FromQuery] int boyut = 10)
        {
            var sonuc = await _siparisService.SiparisleriGetirAsync(durum, santiyeId, sayfa, boyut);
            return Ok(sonuc);
        }

  
        [HttpGet("{id}")]
        public async Task<IActionResult> SiparisDetay(int id)
        {
            var detay = await _siparisService.SiparisDetayGetirAsync(id);
            if (detay == null)
                return NotFound(new { Hata = "Sipariş bulunamadı." });

            return Ok(detay);
        }

        [HttpPost("{id}/sevkiyatlar")]
        public async Task<IActionResult> SevkiyatEkle(int id, [FromBody] SevkiyatEkleModel model)
        {
            try
            {
                var sevkiyat = await _siparisService.SevkiyatEkleAsync(id, model);
                return Ok(sevkiyat);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Hata = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Hata = ex.Message });
            }
        }
        [HttpPut("{id}/iptal")]
        public async Task<IActionResult> SiparisIptal(int id)
        {
            try
            {
                await _siparisService.SiparisIptalEtAsync(id);
                return Ok(new { Mesaj = "Sipariş başarıyla iptal edildi." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Hata = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Hata = ex.Message });
            }
        }
    }
}
