USE [F16I4DABH4Gr3]
GO

CREATE PROCEDURE [dbo].[Procedure_PostData]
(
	@value int,
	@timestamp datetime,
	@sensorId int,
	@apartmentId int
)
AS
BEGIN
	INSERT INTO
		reading (readingValue, readingTimestamp, apartmentId, sensorId)
	VALUES
		(@value, @timestamp, @apartmentId, @sensorId)
END