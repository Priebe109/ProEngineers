﻿IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[Log]'))
	CREATE TABLE Log
	(
	logId BIGINT IDENTITY(1,1) NOT NULL,
	oldValue VARCHAR(1000),
	newValue  VARCHAR(1000)
	)
GO