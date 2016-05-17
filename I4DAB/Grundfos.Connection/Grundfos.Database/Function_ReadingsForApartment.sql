USE [Grundfos.Database]
GO

CREATE FUNCTION [dbo].[ReadingsForApartment]
(
	@apartmentId int
)
RETURNS TABLE
AS
RETURN 
	SELECT
		*
	FROM
		[dbo].[reading]
	WHERE
		apartmentId = @apartmentId