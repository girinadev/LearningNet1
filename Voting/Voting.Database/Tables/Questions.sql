CREATE TABLE [dbo].[Questions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Text] NVARCHAR(MAX) NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Status] SMALLINT NOT NULL, 
    [Type] SMALLINT NOT NULL, 
    [MaxVoteCount] SMALLINT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [MaxAnswersCount] SMALLINT NOT NULL, 
    [VotingEndDate] DATETIME NULL,
    CONSTRAINT FK_Users_Questions FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
)
