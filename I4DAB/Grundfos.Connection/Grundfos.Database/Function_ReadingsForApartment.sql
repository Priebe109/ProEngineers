USE [F16I4DABH4Gr2]
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