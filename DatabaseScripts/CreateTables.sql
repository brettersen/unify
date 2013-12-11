USE [Unify]

DROP TABLE ScheduleTask
DROP TABLE ScheduleTime
DROP TABLE Schedule
DROP TABLE Exemption
DROP TABLE ExemptionEntityOperator
DROP TABLE ExemptionEntity
DROP TABLE ExemptionOperator
DROP TABLE RoutineTask
DROP TABLE Task
DROP TABLE Routine

CREATE TABLE [Routine]
(
	RoutineId				INT				PRIMARY KEY IDENTITY NOT NULL,
	RoutineName				VARCHAR(50)		UNIQUE NOT NULL,
	RoutineDescription		VARCHAR(500)	NULL,
	CreatedOn				DATETIME2		DEFAULT GETDATE() NOT NULL,
	UpdatedOn				DATETIME2		DEFAULT GETDATE() NOT NULL
)

CREATE TABLE [Task]
(
	TaskId					INT				PRIMARY KEY IDENTITY NOT NULL,
	TaskName				VARCHAR(50)		NULL,
	TaskDescription			VARCHAR(500)	NULL,
	SourceDirectory			VARCHAR(255)	NOT NULL,
	DestinationDirectory	VARCHAR(255)	NOT NULL,
	AddFiles				BIT				NOT NULL,
	ReplaceFiles			BIT				NOT NULL,
	RemoveFiles				BIT				NOT NULL,
	SearchRecursively		BIT				NOT NULL,
	ExcludeHiddenFiles		BIT				NOT NULL,
	CreatedOn				DATETIME2		DEFAULT GETDATE() NOT NULL,
	UpdatedOn				DATETIME2		DEFAULT GETDATE() NOT NULL
)

CREATE TABLE [RoutineTask]
(
	RoutineId				INT				FOREIGN KEY REFERENCES Routine(RoutineId) NOT NULL,
	TaskId					INT				FOREIGN KEY REFERENCES Task(TaskId) NOT NULL,
	TaskIndex				TINYINT			NOT NULL
)

CREATE TABLE [ExemptionEntity]
(
	ExemptionEntityId		INT				PRIMARY KEY NOT NULL,
	EntityName				VARCHAR(50)		UNIQUE NOT NULL
)
INSERT INTO [ExemptionEntity]
SELECT 1, 'File extension'
UNION ALL
SELECT 2, 'File name'
UNION ALL
SELECT 3, 'File path'
UNION ALL
SELECT 4, 'File size'
UNION ALL
SELECT 5, 'Folder name'
UNION ALL
SELECT 6, 'Folder path'

CREATE TABLE [ExemptionOperator]
(
	ExemptionOperatorId		INT				PRIMARY KEY NOT NULL,
	OperatorName			VARCHAR(50)		UNIQUE NOT NULL
)
INSERT INTO [ExemptionOperator]
SELECT 1, 'contains'
UNION ALL
SELECT 2, 'is equal to'
UNION ALL
SELECT 3, 'is greater than'
UNION ALL
SELECT 4, 'is less than'
UNION ALL
SELECT 5, 'is not equal to'
UNION ALL
SELECT 6, 'matches'

CREATE TABLE [ExemptionEntityOperator]
(
	ExemptionEntityId		INT				FOREIGN KEY REFERENCES ExemptionEntity(ExemptionEntityId) NOT NULL,
	ExemptionOperatorId		INT				FOREIGN KEY REFERENCES ExemptionEntity(ExemptionEntityId) NOT NULL
)
INSERT INTO [ExemptionEntityOperator]
SELECT 1, 1 UNION ALL SELECT 1, 2 UNION ALL SELECT 1, 5 UNION ALL SELECT 1, 6
UNION ALL
SELECT 2, 1 UNION ALL SELECT 2, 2 UNION ALL SELECT 2, 5 UNION ALL SELECT 2, 6
UNION ALL
SELECT 3, 1 UNION ALL SELECT 3, 2 UNION ALL SELECT 3, 5 UNION ALL SELECT 3, 6
UNION ALL
SELECT 4, 2 UNION ALL SELECT 4, 3 UNION ALL SELECT 4, 4
UNION ALL
SELECT 5, 1 UNION ALL SELECT 5, 2 UNION ALL SELECT 5, 5 UNION ALL SELECT 5, 6
UNION ALL
SELECT 6, 1 UNION ALL SELECT 6, 2 UNION ALL SELECT 6, 5 UNION ALL SELECT 6, 6

CREATE TABLE [Exemption]
(
	ExemptionId				INT				PRIMARY KEY IDENTITY NOT NULL,
	TaskId					INT				FOREIGN KEY REFERENCES Task(TaskId) NOT NULL,
	ExemptionEntityId		INT				FOREIGN KEY REFERENCES ExemptionEntity(ExemptionEntityId) NOT NULL,
	ExemptionOperatorId		INT				FOREIGN KEY REFERENCES ExemptionOperator(ExemptionOperatorId) NOT NULL,
	ExemptionIndex			INT				NOT NULL,
	ExemptionValue			VARCHAR(255)	NOT NULL
)

CREATE TABLE [Schedule]
(
	ScheduleId				INT				PRIMARY KEY IDENTITY NOT NULL,
	ScheduleName			VARCHAR(100)	UNIQUE NOT NULL,
	ScheduleDescription		VARCHAR(500)	NULL,
	CreatedOn				DATETIME2		DEFAULT GETDATE() NOT NULL,
	UpdatedOn				DATETIME2		DEFAULT GETDATE() NOT NULL
)

CREATE TABLE [ScheduleTime]
(
	ScheduleId				INT				FOREIGN KEY REFERENCES Schedule(ScheduleId) NOT NULL,
	ScheduleDays			TINYINT			NOT NULL,
	ScheduleTime			SMALLINT		NOT NULL
)

CREATE TABLE [ScheduleTask]
(
	ScheduleId				INT				FOREIGN KEY REFERENCES Schedule(ScheduleId) NOT NULL,
	TaskId					INT				FOREIGN KEY REFERENCES Task(TaskId) NOT NULL,
	TaskIndex				TINYINT			NOT NULL
)

