USE [F16I4DABH4Gr3]
GO

CREATE VIEW View_ReadingMetaData
AS
SELECT
	TABLE_CATALOG,
	TABLE_SCHEMA,
	TABLE_NAME,
	COLUMN_NAME,
	DATA_TYPE
FROM
	[F16I4DABH4Gr3].INFORMATION_SCHEMA.COLUMNS
WHERE
	TABLE_NAME = 'reading'

/* Previous attempt (using reading function data)

CREATE VIEW View_ReadingMetaData
AS
SELECT
	COUNT(readingValue),
	AVG(readingValue),
	MAX(readingValue),
	MIN(readingValue),
	MAX(readingTimestamp)
FROM
	reading
*/