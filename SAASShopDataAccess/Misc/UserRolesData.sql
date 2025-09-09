INSERT INTO [dbo].[AppUsers]([LoginId],[Password],[FirstName],[LastName],[Email],[Phone])
VALUES('CodeInDiary','UVxreSILr6mRiSG80ONuoO96P2bpa1koD7JbIsv8028=','Code In','Diary','kslimon41@gmail.com','01709447040')

INSERT INTO [dbo].[AppRoles]([Id],[Description],[Role]) VALUES (1,'Super Administrator','SuperAdmin')
INSERT INTO [dbo].[AppRoles]([Id],[Description],[Role]) VALUES (2,'Owner','Owner')
INSERT INTO [dbo].[AppRoles]([Id],[Description],[Role]) VALUES (3,'Shop Staff','ShopStaff')
INSERT INTO [dbo].[AppUserRoles] ([AppUserId], [AppRoleId]) VALUES(1,1)
INSERT INTO [dbo].[AppUserModules] ([AppUserId], [AppRoleId], [ModuleId], [HasAccess]) VALUES(1,1,0,'True')



INSERT INTO [dbo].[SAASShopProducts]
           ([Name]
           ,[Version]
           ,[DisplayName]
           ,[ServerName]
           ,[LastUpdated])
     VALUES
           ('SAASShop','1.00','SAASShop','SAAS Shop Local', '2024-02-22')
GO

-- System Configurations scripts
INSERT INTO [dbo].[SystemConfigurations] ([Code],[Value],[IsActive]) VALUES(0x0101,14,1)
INSERT INTO [dbo].[SystemConfigurations] ([Code],[Value],[IsActive]) VALUES(0x0301,36,0)
INSERT INTO [dbo].[SystemConfigurations] ([Code],[Value],[IsActive]) VALUES(0x0302,36,0)
INSERT INTO [dbo].[SystemConfigurations] ([Code],[Value],[IsActive]) VALUES(0x0501,0,0)
INSERT INTO [dbo].[SystemConfigurations] ([Code],[Value],[IsActive]) VALUES(0x0502,60,0)

GO

CREATE TABLE [dbo].[AutoGen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastCode] [nvarchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[Description] [nvarchar](250) NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[AutoGen]([LastCode], [Type], [Description]) VALUES ('', 1, 'Owner Number')
INSERT INTO [dbo].[AutoGen]([LastCode], [Type], [Description]) VALUES ('', 1, 'Shop Number')

GO
---------------------------------------------------------------------------------------------------------------------------------------
-- Get Local Date
---------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER FUNCTION [dbo].[GetLocalDate]()
RETURNS datetime2
AS
	BEGIN
	  RETURN CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Central Standard Time' AS datetime)
	END;
GO

