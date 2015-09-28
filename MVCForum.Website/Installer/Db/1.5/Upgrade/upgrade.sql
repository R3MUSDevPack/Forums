ALTER TABLE Category
ADD ModerateTopics bit NULL, ModeratePosts bit NULL, PageTitle nvarchar(80) NULL, MetaDescription nvarchar(200) NULL, [Path] nvarchar(2500) NULL
GO

ALTER TABLE Post
ADD Pending bit NULL
GO

ALTER TABLE Topic
ADD Pending bit NULL
GO

ALTER TABLE PrivateMessage
ADD IsSentMessage bit NULL
GO

ALTER TABLE Badge
ADD AwardsPoints int NULL
GO

ALTER TABLE Settings
ADD [PageTitle] nvarchar(80) NULL, [MetaDesc] nvarchar(200) NULL
GO

	
