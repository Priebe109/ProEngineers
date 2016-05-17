USE [F16I4DABH4Gr2]
GO

CREATE TRIGGER [Trigger]
	ON [dbo].[reading]
	FOR DELETE, INSERT, UPDATE
	AS
BEGIN
DECLARE @oldValue nvarchar(1000);
DECLARE @newValue nvarchar(1000);
SELECT @oldValue = d.readingValue from deleted d;
SELECT @newValue = i.readingValue from inserted i;
INSERT INTO LOG (oldValue, newValue) values (@oldValue, @newValue)
END