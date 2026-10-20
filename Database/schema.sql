CREATE DATABASE Mikrotek;
GO

USE Mikrotek;
GO

CREATE TABLE Musteri (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Unvan NVARCHAR(200) NOT NULL,
    VergiNo NVARCHAR(20) NOT NULL,
    Telefon NVARCHAR(20) NOT NULL,
    KayitTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT vergiNo UNIQUE (VergiNo)
);

CREATE TABLE Santiye (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MusteriId INT NOT NULL,
    Ad NVARCHAR(150) NOT NULL,
    Adres NVARCHAR(500) NOT NULL,
    Aktif BIT NOT NULL DEFAULT 1,
    CONSTRAINT santiyeMusteri FOREIGN KEY (MusteriId) REFERENCES Musteri(Id)
);

CREATE TABLE BetonSinifi (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Kod NVARCHAR(20) NOT NULL,
    BirimFiyat DECIMAL(18,2) NOT NULL,
    CONSTRAINT betonKod UNIQUE (Kod),
    CONSTRAINT fiyatPozitif CHECK (BirimFiyat > 0)
);

CREATE TABLE Siparis (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SantiyeId INT NOT NULL,
    BetonSinifiId INT NOT NULL,
    MiktarM3 DECIMAL(18,2) NOT NULL,
    TeslimTarihi DATETIME2 NOT NULL,
    Durum NVARCHAR(20) NOT NULL DEFAULT N'Beklemede',
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT siparisSantiye FOREIGN KEY (SantiyeId) REFERENCES Santiye(Id),
    CONSTRAINT siparisBeton FOREIGN KEY (BetonSinifiId) REFERENCES BetonSinifi(Id),
    CONSTRAINT miktarPozitif CHECK (MiktarM3 > 0),
    CONSTRAINT durumKontrol CHECK (Durum IN (N'Beklemede', N'Sevkiyatta', N'Tamamlandý', N'Ýptal'))
);

CREATE TABLE Sevkiyat (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SiparisId INT NOT NULL,
    IrsaliyeNo NVARCHAR(50) NOT NULL,
    Plaka NVARCHAR(20) NOT NULL,
    MiktarM3 DECIMAL(18,2) NOT NULL,
    CikisZamani DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT sevkiyatSiparis FOREIGN KEY (SiparisId) REFERENCES Siparis(Id),
    CONSTRAINT irsaliyeNo UNIQUE (IrsaliyeNo),
    CONSTRAINT sevkiyatMiktarSiniri CHECK (MiktarM3 > 0 AND MiktarM3 <= 12)
);

CREATE INDEX idxMusteri ON Santiye(MusteriId);
CREATE INDEX idxSiparisDurum ON Siparis(SantiyeId, Durum);
CREATE INDEX idxTeslimTarihi ON Siparis(TeslimTarihi);
CREATE INDEX idxSevkiyatSiparis ON Sevkiyat(SiparisId);

GO