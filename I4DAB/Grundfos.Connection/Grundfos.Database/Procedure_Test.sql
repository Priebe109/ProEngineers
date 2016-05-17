/* Gets readings from specified apartment id */
EXECUTE Procedure_GetData 69
GO

/* Posts reading */
EXECUTE Procedure_PostData @value = 10, @timestamp = GETDATE, @sensorId = 10, @apartmentId = 10
GO