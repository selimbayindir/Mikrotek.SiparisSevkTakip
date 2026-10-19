USE Mikrotek;
GO

INSERT INTO Musteri (Unvan, VergiNo, Telefon) VALUES
(N'A', N'1111111111', N'02122122122'),
(N'B', N'2222222222', N'02122122122'),
(N'C', N'3333333333', N'02122122122');

INSERT INTO Santiye (MusteriId, Ad, Adres, Aktif) VALUES
(1, N'Sancaktepe', N'Sancaktepe Adres Bilgisi', 1),
(1, N'Ataþehir', N'Ataþehir Adres Bilgisi', 1),
(2, N'Gebze', N'Gebze Adres Bilgisi', 1),
(2, N'Kadýkoy', N'Kadýkoy Adres Bilgisi', 1),
(3, N'Sakarya', N'Sakarya Adres Bilgisi', 1);

INSERT INTO BetonSinifi (Kod, BirimFiyat) VALUES
(N'C25', 2200.00),
(N'C30', 2500.00),
(N'C35', 2800.00),
(N'C40', 3000.00);

INSERT INTO Siparis (SantiyeId, BetonSinifiId, MiktarM3, TeslimTarihi, Durum) VALUES
(1, 1, 11.00, DATEADD(DAY, -5, GETDATE()), N'Tamamlandý'),
(1, 2, 12.00, DATEADD(DAY, 2, GETDATE()), N'Sevkiyatta'),
(2, 2, 12.00, DATEADD(DAY, -3, GETDATE()), N'Sevkiyatta'),
(3, 3, 12.00, DATEADD(DAY, -10, GETDATE()), N'Tamamlandý'),
(4, 1, 9.00, DATEADD(DAY, 5, GETDATE()), N'Beklemede'),
(5, 4, 6.00, DATEADD(DAY, -2, GETDATE()), N'Sevkiyatta'),
(3, 2, 10.00, DATEADD(DAY, 1, GETDATE()), N'Ýptal'),
(5, 1, 12.00, DATEADD(DAY, 3, GETDATE()), N'Sevkiyatta');

INSERT INTO Sevkiyat (SiparisId, IrsaliyeNo, Plaka, MiktarM3, CikisZamani) VALUES
(1, N'IRS-001', N'34ABC01', 5.00, DATEADD(DAY, -5, GETDATE())),
(1, N'IRS-002', N'34ABC02', 6.00, DATEADD(DAY, -5, GETDATE())),
(2, N'IRS-003', N'34ABC01', 6.00, DATEADD(DAY, -1, GETDATE())),
(2, N'IRS-004', N'34ABC03', 4.00, DATEADD(DAY, -1, GETDATE())),
(3, N'IRS-005', N'34ABC02', 8.00, DATEADD(DAY, -4, GETDATE())),
(4, N'IRS-006', N'34ABC04', 7.00, DATEADD(DAY, -10, GETDATE())),
(4, N'IRS-007', N'34ABC04', 5.00, DATEADD(DAY, -10, GETDATE())),
(6, N'IRS-008', N'34ABC05', 2.00, DATEADD(DAY, -2, GETDATE())),
(6, N'IRS-009', N'34ABC05', 2.00, DATEADD(DAY, -2, GETDATE())),
(8, N'IRS-010', N'34ABC06', 2.00, DATEADD(DAY, -1, GETDATE())),
(8, N'IRS-011', N'34ABC06', 2.00, DATEADD(DAY, -1, GETDATE())),
(8, N'IRS-012', N'34ABC06', 2.00, DATEADD(DAY, -1, GETDATE())),
(8, N'IRS-013', N'34ABC06', 2.00, DATEADD(DAY, -1, GETDATE())),
(8, N'IRS-014', N'34ABC06', 2.00, DATEADD(DAY, -1, GETDATE())),
(8, N'IRS-015', N'34ABC06', 1.00, DATEADD(DAY, -1, GETDATE()));
GO