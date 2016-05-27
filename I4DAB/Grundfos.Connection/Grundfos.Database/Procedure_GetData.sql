USE [F16I4DABH4Gr3]
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