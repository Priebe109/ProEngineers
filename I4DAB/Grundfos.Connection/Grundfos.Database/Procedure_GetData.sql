USE [Grundfos.Database]
GO

CREATE PROCEDURE [dbo].[Procedure_GetData]
	@apartmentId int
AS
BEGIN
	SELECT
		*
	FROM
		[dbo].[ReadingsForApartment](@apartmentId)
	ORDER BY
		readingTimestamp ASC
END