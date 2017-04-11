create database exam
go

use exam
go


CREATE TABLE [cadre_info](
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[UserID]		[nvarchar](20) NOT NULL,
	[UserName]		[nvarchar](50) NULL,
	[Pwd]			[nvarchar](50) NOT NULL,
	[DeptId]		[int] NOT NULL,
	[Office]		[nvarchar](50) NULL,
	[Duties]		[nvarchar](50) NULL,
	[Rank]			[nvarchar](50) NULL,
	[Post]			[nvarchar](50) NULL,
	[Spower]		[nvarchar](50) NULL,
	PRIMARY KEY ([Id])
) 
GO

CREATE UNIQUE INDEX [IX_UserId] ON [cadre_info]
([UserID] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO


CREATE INDEX [IX_Pwd] ON [cadre_info]
([Pwd] ASC) 
GO


CREATE TABLE [department](
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[PId]			[int] NOT NULL,
	[DeptName]		[nvarchar](50) NULL
) ON [PRIMARY]

GO



CREATE TABLE [exam_database](
	[Id]			[int] IDENTITY(1,1) NULL,
	[DeptId]		[int] NOT NULL,
	[SubjectId]		[int] NOT NULL,
	[Question]		[nvarchar(MAX)] NOT NULL,
	[Type]			[nvarchar](10) NOT NULL,
	[Text1]			[nvarchar](255) NULL,
	[Text2]			[nvarchar](255) NULL,
	[Text3]			[nvarchar](255) NULL,
	[Text4]			[nvarchar](255) NULL,
	[Text5]			[nvarchar](255) NULL,
	[Text6]			[nvarchar](255) NULL,
	[Answer]		[nvarchar](255) NULL,
	PRIMARY KEY ([Id])
)
GO

CREATE INDEX [IX_DeptId] ON [exam_database]
([DeptId] ASC) 
GO
CREATE INDEX [IX_SubjectId] ON [exam_database]
([SubjectId] ASC) 
GO
CREATE INDEX [IX_Type] ON [exam_database]
([Type] ASC) 
GO


CREATE TABLE [exam_news](
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[NewsId]		[int] NULL,
	[Title]			[nvarchar](max) NULL,
	[Text]			[nvarchar](max) NULL,
	[Youxq]			[int] NULL,
	[Fabsj]			[datetime] NULL,
	[Fabr]			[nvarchar](50) NULL,
	[PassUserId]	[int] NULL,
	[PassTime] [nvarchar](50) NULL,
	PRIMARY KEY ([Id])
)

GO


CREATE TABLE [exam_news_log](
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[NewsId]		[int] NULL,
	[UserId]		[int] NULL,
	PRIMARY KEY ([Id])
)

GO


CREATE TABLE [exam_score](
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[UserId]		[nvarchar](20) NOT NULL,
	[UserName]		[nvarchar](50) NOT NULL,
	[DeptId]		[int] NOT NULL,
	[Office]		[nvarchar](50) NULL,
	[Duties]		[nvarchar](50) NULL,
	[SubjectId]		[int] NOT NULL,
	[TestId]		[int] NOT NULL,
	[StartTime]		[datetime] NULL,
	[EndTime]		[datetime] NULL,
	[Score]			[int] NULL,
	[Wrong_Dan]		[nvarchar](max) NULL,
	[Wrong_Duo]		[nvarchar](max) NULL,
	[Wrong_Pd]		[nvarchar](max) NULL,
	PRIMARY KEY ([Id])
) 

GO

CREATE INDEX [IX_UserId] ON [exam_score]
([UserId] ASC) 
GO

CREATE INDEX [IX_TestId] ON [exam_score]
([TestId] ASC) 
GO



CREATE TABLE [exam_subject](
	[Id]				[int] IDENTITY(1,1) NOT NULL,
	[SubjectName]		[nvarchar](50) NULL,
	PRIMARY KEY ([Id])
)
GO


CREATE TABLE [exam_test](
	[Id]				[int] IDENTITY(1,1) NOT NULL,
	[TestId]			[int] NOT NULL,
	[SubjectId]			[int] NOT NULL,
	[TotalPer]			[decimal(18,2)] NOT NULL,
	[SingleCount]		[int] NOT NULL,
	[SinglePer]			[decimal(18,2)] NOT NULL,
	[MultiCount]		[int] NOT NULL,
	[MultiPer]			[decimal(18,2)] NOT NULL,
	[JudgeCount]		[int] NOT NULL,
	[JudgePer]			[[decimal(18,2)] NOT NULL,
	[TestTime]			[int] NOT NULL,
	[StartTime]			[datetime] NOT NULL,
	[EndTime]			[datetime] NOT NULL,
	[SetTime]			[datetime] NOT NULL,
	[SetUserId]			[int] NULL,
	[PassUserId]		[int] NULL,
	[PassTime]			[datetime] NULL,
	PRIMARY KEY ([Id])
) 
GO

CREATE INDEX [IX_TestId] ON [exam_test]
([TestId] ASC) 
GO



CREATE TABLE [exam_testuser](
	[Id]			[int] IDENTITY(1,1) NOT NULL, 
	[TestId]		[int] NOT NULL,
	[UserId]		[nvarchar](20) NOT NULL,
	[HaveTest]		[int] NOT NULL default 0,
	PRIMARY KEY ([Id])
)
GO
CREATE INDEX [IX_TestId] ON [exam_testuser]
([TestId] ASC) 
GO

CREATE INDEX [IX_UserId] ON [exam_testuser]
([UserId] ASC) 
GO


CREATE TABLE [waitforpass](
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[ObjName]		[nvarchar](50) NULL,
	[ObjId]			[int] NULL,
	[PassUserId]	[int] NULL,
	[PassAdvice]	[nvarchar](max) NULL,
	[PassTime]		[datetime] NULL,
	[HavePass]		[int] NULL,
	PRIMARY KEY ([Id])
) 
GO
CREATE INDEX [IX_TestId] ON [exam_testuser]
([TestId] ASC) 
GO

CREATE INDEX [IX_UserId] ON [exam_testuser]
([UserId] ASC) 
GO


CREATE TABLE [AdminUser](
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[UserName] 		[nvarchar](50) NOT NULL,
	[UserPwd]		[nvarchar](50) NOT NULL,
	[DeptId]		[int] NOT NULL,
	[IsSa]			[bit] NOT NULL,
	PRIMARY KEY ([Id])
) 
GO