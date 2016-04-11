CREATE TABLE [dbo].[Person] (
    [PersonID]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Efternavn]  NVARCHAR (500) NOT NULL,
    [Fornavn]    NVARCHAR (500) NOT NULL,
    [Mellemnavn] NVARCHAR (500) NOT NULL,
    [Type]       NVARCHAR (500) NOT NULL,
    [AdresseID]  BIGINT         NOT NULL,
    CONSTRAINT [pk_Person] PRIMARY KEY CLUSTERED ([PersonID] ASC),
    CONSTRAINT [fk_Person] FOREIGN KEY ([AdresseID]) REFERENCES [dbo].[Adresse] ([AdresseID]) ON UPDATE CASCADE
);

