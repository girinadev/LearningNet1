CREATE TABLE [dbo].[Answers]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Text] NVARCHAR(MAX) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [QuestionId] UNIQUEIDENTIFIER NOT NULL, 
    [UpdatedDate] NCHAR(10) NULL,
    CONSTRAINT FK_Answers_Questions FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions] ([Id]) ON DELETE CASCADE
)
