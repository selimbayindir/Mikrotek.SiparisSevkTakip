using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mikrotek.SiparisSevkTakip.DataAccess.Context;

namespace Mikrotek.SiparisSevkTakip.API.Controllers
{
 
    [ApiController]
    [Route("api/musteriler")]
    public class MusterilerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MusterilerController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<IActionResult> MusterileriGetir()
        {
            var musteriler = await _context.Musteri.ToListAsync();
            return Ok(musteriler);
        }

    
        [HttpGet("{id}/santiyeler")]
        public async Task<IActionResult> MusteriSantiyeleriniGetir(int id)
        {
            bool musteriVarMi = await _context.Musteri.AnyAsync(m => m.Id == id);
            if (!musteriVarMi)
                return NotFound(new { Hata = "Müşteri bulunamadı." });

            var santiyeler = await _context.Santiye.Where(s => s.MusteriId == id).ToListAsync();
            return Ok(santiyeler);
        }
    }
}
