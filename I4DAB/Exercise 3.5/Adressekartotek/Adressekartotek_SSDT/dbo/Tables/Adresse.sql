CREATE TABLE [dbo].[Adresse] (
    [AdresseID]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Bynavn]     NVARCHAR (500) NOT NULL,
    [Husnummer]  BIGINT         NOT NULL,
    [Postnummer] BIGINT         NOT NULL,
    [Type]       NVARCHAR (500) NOT NULL,
    [Vejnavn]    NVARCHAR (500) NOT NULL,
    CONSTRAINT [pk_Adresse] PRIMARY KEY CLUSTERED ([AdresseID] ASC)
);

