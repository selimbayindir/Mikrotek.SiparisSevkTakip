using Mikrotek.SiparisSevkTakip.Business;
using Mikrotek.SiparisSevkTakip.DataAccess.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// 1. Servislerin Konfigürasyonu (AddDbContext, AddControllers vb. hepsi burada bir kez olmalý)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<SiparisService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Uygulamanýn Ýnþa Edilmesi (Builder sadece bir kez çaðrýlýr)
var app = builder.Build();

// 3. HTTP Ýstek Boru Hattýnýn (Pipeline) Yapýlandýrýlmasý
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();