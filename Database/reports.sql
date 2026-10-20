

/*
*   1.
    Her sipariþin sipariþ edilen, gönderilen ve kalan m3 miktarý. 
    Hiç sevkiyatý olmayan sipariþler de listelenmelidir.
*/
SELECT 
    S.Id AS [Sipariþ Nu],
    S.MiktarM3 AS [Sipariþ M3],
    ISNULL(SUM(SV.MiktarM3), 0) AS [Gönderilen M3],
   S.MiktarM3 - ISNULL(SUM(SV.MiktarM3), 0) AS [Kalan M3],
    s.Durum [Sipariþ Durumu]
FROM Siparis S
LEFT JOIN Sevkiyat SV ON S.Id = SV.SiparisId
GROUP BY S.Id, S.Durum, S.MiktarM3;

-----------------------------------
/*
*   2.
    Son 30 günde müþteri bazýnda toplam gönderilen m3 ve tutar (miktar × birim fiyat), 
    tutara göre azalan sýrada.
*/

-----------------------------------
SELECT 
    M.Id AS [Müþteri Nu],

    M.Unvan AS [Unvan],

    SUM(SV.MiktarM3) AS [Toplam Gönderilen M3],

    SUM(SV.MiktarM3 * B.BirimFiyat) AS [Toplam Tutar]

FROM Sevkiyat SV

INNER JOIN Siparis S ON SV.SiparisId = S.Id

INNER JOIN BetonSinifi B ON S.BetonSinifiId = B.Id

INNER JOIN Santiye SA ON S.SantiyeId = SA.Id

INNER JOIN Musteri M ON SA.MusteriId = M.Id

WHERE SV.CikisZamani >= DATEADD(DAY, -30, GETDATE())

GROUP BY M.Id, M.Unvan

ORDER BY [Toplam Tutar] DESC;

-----------------------------------
/*
*   3.
     Teslim tarihi geçmiþ ama tamamlanmamýþ sipariþler ve kaç gün geciktikleri
*/

-----------------------------------
SELECT 
    S.Id AS [Sipariþ Nu],
    SA.Ad AS [Þantiye Adý],
    S.MiktarM3,
    S.TeslimTarihi AS [Teslim Tarihi],
    S.Durum [Durum],
    DATEDIFF(DAY, S.TeslimTarihi, GETDATE()) AS GecikmeGunSayisi
FROM Siparis S
INNER JOIN Santiye SA ON S.SantiyeId = SA.Id
WHERE S.TeslimTarihi < GETDATE()
  AND S.Durum NOT IN (N'Tamamlandý', N'Ýptal');
  -----------------------------------
/*
*   4.
     Beton sýnýfý bazýnda aylýk toplam gönderilen m³ (yýl, ay, sýnýf kýrýlýmýnda).
*/

-----------------------------------
SELECT 
    YEAR(SV.CikisZamani) AS [Yýl],

    MONTH(SV.CikisZamani) AS [Ay],

    b.Kod AS BetonSinifi,

    SUM(SV.MiktarM3) AS [Toplam Gönderilen M3]

FROM Sevkiyat SV

INNER JOIN Siparis S ON SV.SiparisId = S.Id
INNER JOIN BetonSinifi B ON S.BetonSinifiId = B.Id
GROUP BY YEAR(SV.CikisZamani), MONTH(SV.CikisZamani), B.Kod
ORDER BY [Yýl] DESC, Ay DESC, B.Kod;



