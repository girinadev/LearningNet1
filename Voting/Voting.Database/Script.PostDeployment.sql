/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_Users_Questions]
ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_Answers_Questions]
ALTER TABLE [dbo].[Votes] DROP CONSTRAINT [FK_Votes_Answers]
ALTER TABLE [dbo].[Votes] DROP CONSTRAINT [FK_Users_Votes]
GO
INSERT INTO [dbo].[Users] ([Id], [Email], [FirstName], [LastName], [Password], [CreatedDate]) VALUES (N'0fbad71a-b894-4836-ae7a-9ef5c845f31a', N'test2@test.com', N'Nataly', N'Portman', N'�r�ԹE3�H��=�ƒ�݉r4���?r~.kc', '20200423 14:41:48.883')
INSERT INTO [dbo].[Users] ([Id], [Email], [FirstName], [LastName], [Password], [CreatedDate]) VALUES (N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'test@test.com', N'Pavel', N'Grigoriev', N'�r�ԹE3�H��=�ƒ�݉r4���?r~.kc', '20200423 14:09:58.053')
INSERT INTO [dbo].[Users] ([Id], [Email], [FirstName], [LastName], [Password], [CreatedDate]) VALUES (N'af0eeab4-c3cc-44cd-af72-f3b1ce0a4f27', N'test1@test.com', N'Susanne', N'Klatten', N'�r�ԹE3�H��=�ƒ�݉r4���?r~.kc', '20200423 14:27:40.893')
GO
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'1904a1b8-58f5-4d1d-babc-257317d2428c', N'af0eeab4-c3cc-44cd-af72-f3b1ce0a4f27', N'f4a5f495-7c8d-4ed2-8ea4-e3b7ec2ee0a5', '20200423 15:16:15.600')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'4388d31c-1747-487c-915d-4160560c7d84', N'af0eeab4-c3cc-44cd-af72-f3b1ce0a4f27', N'de726af1-dfc1-4a1b-b601-0083dc00f3e9', '20200423 14:37:14.997')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'4721b501-d46d-453e-bb9e-ae501e7dc8f9', N'af0eeab4-c3cc-44cd-af72-f3b1ce0a4f27', N'2c79bc17-df73-4fdc-9e53-c412d1f4d525', '20200423 14:27:54.887')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'6cc1b474-9246-4e3c-9423-c09001c45d87', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'43618ab7-3969-485f-be10-3eefb5ca7d02', '20200423 14:45:20.153')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'7ced3875-d734-48d7-86ff-1c7222c1e3e8', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'e5bde85c-ba7a-4815-bf5e-3652fc71fd78', '20200423 14:45:31.383')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'86b61456-65df-4a79-ac30-31393421999c', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'26a25de9-07ec-4934-8909-e786e84a42a5', '20200423 14:38:03.187')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'a1258746-8a9a-493d-a5c2-61e5819a96f1', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'40fd3503-29f0-4ee1-9b53-3a0235556795', '20200423 14:47:04.897')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'b88c6175-801c-43c7-be6e-2189a658c58a', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'46824c2b-595c-4c11-ad41-c548236e562f', '20200423 14:37:33.480')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'd7f42640-3e07-4fb5-b127-13c7caeee9d2', N'af0eeab4-c3cc-44cd-af72-f3b1ce0a4f27', N'7075d4c4-f62c-4af4-8260-c0dad5d615a4', '20200423 15:16:15.590')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'dab3f6e7-46c7-4a25-9a54-0fdcf76dbf4c', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'8fd4c077-c9f0-4a03-ba0d-ac671c0ccc30', '20200423 14:45:31.377')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'dffc9e43-9df5-40f8-a1bc-f8918b1508cb', N'0fbad71a-b894-4836-ae7a-9ef5c845f31a', N'0d92c073-222e-4973-8e54-6f7993028c47', '20200423 14:44:32.530')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'e71dde2f-8d40-40f4-b78f-4430be670822', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'dbe0f797-58f1-473f-bc5e-6a22ffc4bce6', '20200423 14:37:33.470')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'f3b47273-b363-44c9-aef7-5361b26b7086', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', N'3a1710ea-7d40-4f61-90c2-3419afa2d45f', '20200423 14:40:32.190')
INSERT INTO [dbo].[Votes] ([Id], [UserId], [AnswerId], [CreatedDate]) VALUES (N'f74000a2-896a-466f-8f2c-39ee6f7c0b96', N'0fbad71a-b894-4836-ae7a-9ef5c845f31a', N'353f789b-13bc-4aa2-ac1a-fb0f2d475570', '20200423 15:15:39.680')
GO
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'0d92c073-222e-4973-8e54-6f7993028c47', N'Этиловый спирт', '20200423 14:43:48.157', N'84ac85e1-4743-4683-852b-c9d1be23565c', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'26a25de9-07ec-4934-8909-e786e84a42a5', N'English', '20200423 14:12:18.440', N'bb77236b-6842-46a5-99f4-877f26dd3c60', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'2c79bc17-df73-4fdc-9e53-c412d1f4d525', N'JULIA KOCH & FAMILY', '20200423 14:26:18.823', N'f89e6cfd-29fc-4736-9611-0b6adaf3c50e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'353f789b-13bc-4aa2-ac1a-fb0f2d475570', N'Никогда', '20200423 15:15:39.670', N'b19cc0f2-875f-45a2-a7cd-54e958432bd3', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'3a1710ea-7d40-4f61-90c2-3419afa2d45f', N'Наличными', '20200423 14:40:14.090', N'89a8651a-66b3-49ea-844b-42d1a845c84e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'3ef5f8a4-36e6-4072-8171-28a16f5c000f', N'Спасибо', '20200423 14:40:14.090', N'89a8651a-66b3-49ea-844b-42d1a845c84e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'3f01b63c-0bd3-4d7f-9bc2-a30c2acecf59', N'buddy', '20200423 14:36:34.770', N'4448eb79-e752-42f7-a381-9208c7a6224a', N'Apr 23 202')
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'40fd3503-29f0-4ee1-9b53-3a0235556795', N'MACKENZIE BEZOS ', '20200423 14:26:18.827', N'f89e6cfd-29fc-4736-9611-0b6adaf3c50e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'43618ab7-3969-485f-be10-3eefb5ca7d02', N' Structured Query Language', '20200423 14:33:33.080', N'a4068916-e7dc-41e3-b29e-327825f02f8b', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'46824c2b-595c-4c11-ad41-c548236e562f', N'mate', '20200423 14:36:34.773', N'4448eb79-e752-42f7-a381-9208c7a6224a', N'Apr 23 202')
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'479b8233-668e-4ed2-881a-fc64735d605a', N'Вечером', '20200423 15:03:23.363', N'b19cc0f2-875f-45a2-a7cd-54e958432bd3', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'63c02399-c5cc-497a-9b2a-9b14d31ea9c2', N' Structured Question Language', '20200423 14:33:33.083', N'a4068916-e7dc-41e3-b29e-327825f02f8b', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'6b6a9ea1-3b8a-425d-a4d2-0f993a915413', N'Spanish', '20200423 14:12:18.450', N'bb77236b-6842-46a5-99f4-877f26dd3c60', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'7075d4c4-f62c-4af4-8260-c0dad5d615a4', N'Картой', '20200423 14:40:14.087', N'89a8651a-66b3-49ea-844b-42d1a845c84e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'75e69a27-58d3-46cf-9527-22e0bdda024c', N'Осиновый колл', '20200423 14:43:48.157', N'84ac85e1-4743-4683-852b-c9d1be23565c', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'7ec978a7-71b2-459d-acff-d9232105fdd5', N'Большое спасибо', '20200423 14:40:14.093', N'89a8651a-66b3-49ea-844b-42d1a845c84e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'87284c92-c7eb-4d68-a0f8-0b7d1aa13f4b', N'ALICE WALTON', '20200423 14:26:18.820', N'f89e6cfd-29fc-4736-9611-0b6adaf3c50e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'8fd4c077-c9f0-4a03-ba0d-ac671c0ccc30', N'Маска', '20200423 14:43:48.147', N'84ac85e1-4743-4683-852b-c9d1be23565c', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'a5bff484-a034-48da-8641-bb6c10ddbb58', N'Утром', '20200423 15:03:23.363', N'b19cc0f2-875f-45a2-a7cd-54e958432bd3', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'c79a849e-8c18-4bec-b6ec-cb28bfbd58ce', N'Mandarin Chinese', '20200423 14:12:18.440', N'bb77236b-6842-46a5-99f4-877f26dd3c60', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'd0b4b992-9a21-4239-89d0-cb1b955867b9', N'JACQUELINE MARS ', '20200423 14:26:18.827', N'f89e6cfd-29fc-4736-9611-0b6adaf3c50e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'dbe0f797-58f1-473f-bc5e-6a22ffc4bce6', N'friend', '20200423 14:36:34.770', N'4448eb79-e752-42f7-a381-9208c7a6224a', N'Apr 23 202')
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'de726af1-dfc1-4a1b-b601-0083dc00f3e9', N'amigo', '20200423 14:37:14.990', N'4448eb79-e752-42f7-a381-9208c7a6224a', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'e157dfa6-43ea-49e9-8af7-a8d3d4a48771', N'Arabic', '20200423 14:12:18.453', N'bb77236b-6842-46a5-99f4-877f26dd3c60', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'e3e890c5-6021-4142-92e4-c6a1203e19b3', N' Strong Question Language', '20200423 14:33:33.080', N'a4068916-e7dc-41e3-b29e-327825f02f8b', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'e5bde85c-ba7a-4815-bf5e-3652fc71fd78', N'Перчатки', '20200423 14:43:48.153', N'84ac85e1-4743-4683-852b-c9d1be23565c', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'e6913313-4bb8-4840-964d-ef9f3497803a', N'Hindustani', '20200423 14:12:18.447', N'bb77236b-6842-46a5-99f4-877f26dd3c60', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'ec28d8ce-cd8b-4927-93e3-419ee62a4dd2', N'FRANCOISE BETTENCOURT MEYERS & FAMILY', '20200423 14:26:18.820', N'f89e6cfd-29fc-4736-9611-0b6adaf3c50e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'f4a5f495-7c8d-4ed2-8ea4-e3b7ec2ee0a5', N'Бесконтактным чипом', '20200423 14:40:14.090', N'89a8651a-66b3-49ea-844b-42d1a845c84e', NULL)
INSERT INTO [dbo].[Answers] ([Id], [Text], [CreatedDate], [QuestionId], [UpdatedDate]) VALUES (N'ff253099-cf59-49f2-b25d-b3efb2233835', N'Чеснок', '20200423 14:43:48.160', N'84ac85e1-4743-4683-852b-c9d1be23565c', NULL)
GO
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'0586edb6-d50b-45f3-8746-9bb148111ecf', N'Куда поехать в отпуск?', N'0fbad71a-b894-4836-ae7a-9ef5c845f31a', 0, 0, 0, '20200423 15:06:05.707', NULL, 0, NULL)
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'4448eb79-e752-42f7-a381-9208c7a6224a', N'Как на английском слово друг?', N'af0eeab4-c3cc-44cd-af72-f3b1ce0a4f27', 1, 1, 2, '20200423 14:36:34.757', '20200423 14:37:00.643', 1, '20200528 00:00:00.000')
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'84ac85e1-4743-4683-852b-c9d1be23565c', N'Что защищает от COVID-19?', N'0fbad71a-b894-4836-ae7a-9ef5c845f31a', 1, 1, 5, '20200423 14:43:48.140', NULL, 1, '20200511 00:00:00.000')
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'89a8651a-66b3-49ea-844b-42d1a845c84e', N'Как можно рассчитаться в трамвае?', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', 1, 1, 2, '20200423 14:40:14.073', NULL, 2, '20200522 00:00:00.000')
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'a4068916-e7dc-41e3-b29e-327825f02f8b', N'What does SQL stand for?', N'af0eeab4-c3cc-44cd-af72-f3b1ce0a4f27', 1, 0, 1, '20200423 14:33:33.073', NULL, 0, '20200430 00:00:00.000')
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'b19cc0f2-875f-45a2-a7cd-54e958432bd3', N'Когда предпочитаете заниматься спортом?', N'0fbad71a-b894-4836-ae7a-9ef5c845f31a', 1, 1, 2, '20200423 15:03:23.340', NULL, 1, '20200423 00:00:00.000')
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'b73abb92-e86d-45af-a3c3-c765d0b4a179', N'Private question', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', 0, 0, 0, '20200423 14:10:15.103', NULL, 0, NULL)
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'bb77236b-6842-46a5-99f4-877f26dd3c60', N'What language are the most popular?', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', 1, 0, 1, '20200423 14:12:18.427', NULL, 0, '20200430 00:00:00.000')
INSERT INTO [dbo].[Questions] ([Id], [Text], [UserId], [Status], [Type], [MaxVoteCount], [CreatedDate], [UpdatedDate], [MaxAnswersCount], [VotingEndDate]) VALUES (N'f89e6cfd-29fc-4736-9611-0b6adaf3c50e', N'Most rich woman in the world', N'10386bb1-699d-4efb-9d88-4fa1770a69a5', 1, 0, 1, '20200423 14:26:18.807', NULL, 0, '20200428 00:00:00.000')
ALTER TABLE [dbo].[Questions]
    ADD CONSTRAINT [FK_Users_Questions] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Answers]
    ADD CONSTRAINT [FK_Answers_Questions] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Votes]
    ADD CONSTRAINT [FK_Votes_Answers] FOREIGN KEY ([AnswerId]) REFERENCES [dbo].[Answers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Votes]
    ADD CONSTRAINT [FK_Users_Votes] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
COMMIT TRANSACTION
