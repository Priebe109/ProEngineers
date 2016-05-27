USE [F16I4DABH4Gr3]
GO

CREATE FUNCTION [dbo].[ReadingsForApartment]
(
	@apartmentId int
)
RETURNS TABLE5
AS
RETURN 
	SELECT
		*
	FROM
		[dbo].[reading]
	WHERE
		apartmentId = @apartmentId