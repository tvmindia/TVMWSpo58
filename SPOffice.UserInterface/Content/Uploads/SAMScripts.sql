USE [SPApps]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[created by DBA]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[created by DBA](
	[test] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Objects]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Objects](
	[ID] [uniqueidentifier] NOT NULL,
	[AppID] [uniqueidentifier] NOT NULL,
	[ObjectName] [nvarchar](250) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Objects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ObjectsAccess]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjectsAccess](
	[ID] [uniqueidentifier] NOT NULL,
	[ObjectID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[Read] [bit] NOT NULL,
	[Write] [bit] NOT NULL,
	[Delete] [bit] NOT NULL,
	[Special] [bit] NOT NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_ObjectsAccess] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrivilegesView]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrivilegesView](
	[ID] [uniqueidentifier] NOT NULL,
	[AppID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[ModuleName] [nvarchar](250) NULL,
	[AccessDescription] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_PrivilegesView] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [uniqueidentifier] NOT NULL,
	[AppID] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](250) NULL,
	[RoleDescription] [nvarchar](250) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpecialAccessItems]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialAccessItems](
	[ID] [uniqueidentifier] NOT NULL,
	[ObjectID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[AccessCode] [nvarchar](5) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_SpecialAccessItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubObjects]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubObjects](
	[ID] [uniqueidentifier] NOT NULL,
	[ObjectID] [uniqueidentifier] NOT NULL,
	[SubObjName] [nvarchar](250) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_SubObjects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubObjectsAccess]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubObjectsAccess](
	[ID] [uniqueidentifier] NOT NULL,
	[SubObjectID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[Read] [bit] NOT NULL,
	[Wrie] [bit] NOT NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_SubObjectsAccess] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sys_Menu]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Menu](
	[ID] [uniqueidentifier] NOT NULL,
	[LinkName] [nvarchar](250) NULL,
	[LinkDescription] [nvarchar](max) NULL,
	[Type] [nvarchar](3) NULL,
	[Controller] [nvarchar](250) NULL,
	[Action] [nvarchar](250) NULL,
	[Order] [int] NULL,
 CONSTRAINT [PK_Sys_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[LoginName] [nvarchar](250) NOT NULL,
	[UserName] [nvarchar](250) NOT NULL,
	[emailID] [nvarchar](250) NOT NULL,
	[Active] [bit] NOT NULL,
	[Password] [nvarchar](250) NULL,
	[ResetLink] [nvarchar](250) NULL,
	[LinkExpiryTime] [time](7) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersInRoles]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersInRoles](
	[ID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_UsersInRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Applications] FOREIGN KEY([ID])
REFERENCES [dbo].[Applications] ([ID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Applications]
GO
ALTER TABLE [dbo].[Objects]  WITH CHECK ADD  CONSTRAINT [FK_Objects_Applications] FOREIGN KEY([AppID])
REFERENCES [dbo].[Applications] ([ID])
GO
ALTER TABLE [dbo].[Objects] CHECK CONSTRAINT [FK_Objects_Applications]
GO
ALTER TABLE [dbo].[ObjectsAccess]  WITH CHECK ADD  CONSTRAINT [FK_ObjectsAccess_Objects] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Objects] ([ID])
GO
ALTER TABLE [dbo].[ObjectsAccess] CHECK CONSTRAINT [FK_ObjectsAccess_Objects]
GO
ALTER TABLE [dbo].[ObjectsAccess]  WITH CHECK ADD  CONSTRAINT [FK_ObjectsAccess_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[ObjectsAccess] CHECK CONSTRAINT [FK_ObjectsAccess_Roles]
GO
ALTER TABLE [dbo].[PrivilegesView]  WITH CHECK ADD  CONSTRAINT [FK_PrivilegesView_Applications] FOREIGN KEY([AppID])
REFERENCES [dbo].[Applications] ([ID])
GO
ALTER TABLE [dbo].[PrivilegesView] CHECK CONSTRAINT [FK_PrivilegesView_Applications]
GO
ALTER TABLE [dbo].[PrivilegesView]  WITH CHECK ADD  CONSTRAINT [FK_PrivilegesView_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[PrivilegesView] CHECK CONSTRAINT [FK_PrivilegesView_Roles]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Applications] FOREIGN KEY([AppID])
REFERENCES [dbo].[Applications] ([ID])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Applications]
GO
ALTER TABLE [dbo].[SpecialAccessItems]  WITH CHECK ADD  CONSTRAINT [FK_SpecialAccessItems_Objects] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Objects] ([ID])
GO
ALTER TABLE [dbo].[SpecialAccessItems] CHECK CONSTRAINT [FK_SpecialAccessItems_Objects]
GO
ALTER TABLE [dbo].[SubObjects]  WITH CHECK ADD  CONSTRAINT [FK_SubObjects_Objects] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Objects] ([ID])
GO
ALTER TABLE [dbo].[SubObjects] CHECK CONSTRAINT [FK_SubObjects_Objects]
GO
ALTER TABLE [dbo].[SubObjectsAccess]  WITH CHECK ADD  CONSTRAINT [FK_SubObjectsAccess_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[SubObjectsAccess] CHECK CONSTRAINT [FK_SubObjectsAccess_Roles]
GO
ALTER TABLE [dbo].[SubObjectsAccess]  WITH CHECK ADD  CONSTRAINT [FK_SubObjectsAccess_SubObjects] FOREIGN KEY([SubObjectID])
REFERENCES [dbo].[SubObjects] ([ID])
GO
ALTER TABLE [dbo].[SubObjectsAccess] CHECK CONSTRAINT [FK_SubObjectsAccess_SubObjects]
GO
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersInRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [FK_UsersInRoles_Roles]
GO
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersInRoles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [FK_UsersInRoles_Users]
GO
/****** Object:  StoredProcedure [dbo].[AddObjectAccess]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Thomson K Varkey>
-- Create date: <29-06-2017>
-- Description:	<Add Object Access>
-- =============================================
CREATE PROCEDURE [dbo].[AddObjectAccess]
	-- Add the parameters for the stored procedure here
	@AppID uniqueidentifier,
	@RoleID uniqueidentifier,
	@DetailXML xml=null,
	@CreatedBy nvarchar(250),
	@CreatedDate Datetime,
	@Status int output,
	@ID uniqueidentifier output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	BEGIN TRANSACTION [Tran1]
		BEGIN TRY
		Delete ObjA from dbo.ObjectsAccess ObjA Inner Join dbo.[Objects] Obj ON Obj.ID=ObjA.ObjectID 
		where Obj.AppID=@AppID AND ObjA.RoleID=@RoleID
			set @ID=newid()			
			------------ details-----------
			declare @temp table(
			 
			ID uniqueidentifier,
			ObjectID uniqueidentifier,
			RoleID uniqueidentifier,			
			[Read] bit,
			Write bit,
			[Delete] bit,
			CreatedBy nvarchar,
			CreatedDate Datetime,
			isProcessed bit,
			tmpID uniqueidentifier
			);

			------------parse from xml to temptable ----
			insert into @temp(ObjectID,RoleID,[Read],Write,[Delete],CreatedBy,CreatedDate,isProcessed,ID,tmpID)
			select [xmlData].[Col].value('./@ObjectID', 'uniqueidentifier') as ObjectID,			
			[xmlData].[Col].value('./@RoleID', 'uniqueidentifier') as RoleID,
			[xmlData].[Col].value('./@Read', 'bit') as [Read],
			[xmlData].[Col].value('./@Write', 'bit') as Write,
			[xmlData].[Col].value('./@Delete', 'bit') as [Delete],	
			[xmlData].[Col].value('./@CreatedBy', 'nvarchar(20)') as CreatedBy,	
			[xmlData].[Col].value('./@CreatedDate', 'DateTime') as CreatedDate,	
			0,newid(),newid()
			from @DetailXML.nodes('/Details/item') as [xmlData]([Col]);



			---- loop temp and insert each detail row ---------
			declare @loopID uniqueidentifier
			declare @count int
			declare @itemNo int		
			declare @tmpMaterial nvarchar(20)	
			declare @tmpQty int

			select @count=count(*) from @temp where isProcessed=0
			set @itemNo=1

			while(@count>0)
			begin
				--1------------------------------------
				select top 1 @loopID=tmpid from @temp where isProcessed=0

				--2------------------------------------
				insert into [dbo].ObjectsAccess
				([ID],[ObjectID],[RoleID],[Read],[Write],[Delete],[Special],[CreatedBy],[CreatedDate])
				select ID,ObjectID,RoleID,[Read],Write,[Delete],0,@CreatedBy,@CreatedDate
				from @temp where tmpID=@loopID

				--3-------------------------------------
				--select @tmpQty=isnull(Quantity,0) from @temp where tmpID=@loopID

				
				

				--5-------------------------------------
				update @temp set isProcessed=1  where tmpID=@loopID
				select @count=count(*) from @temp where isProcessed=0
				set @itemNo=@itemNo+1
				
			end

			--4-------- LEDGER ENTRY----------------
			--EXEC [UpdatePaymentLedger] @SCCode,@ID,'ICRIN',@STAmount,@PaymentMode,@CreatedBy

			set @Status=1
			COMMIT TRANSACTION [Tran1]

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION [Tran1]
			DECLARE @ErrorMessage NVARCHAR(4000); 
			select @ErrorMessage=ERROR_MESSAGE()
			RAISERROR( @ErrorMessage,16,1) 
		END CATCH  
END



GO
/****** Object:  StoredProcedure [dbo].[AddSubObjectAccess]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Thomson K Varkey>
-- Create date: <30-06-2017>
-- Description:	<Add Sub-Object Access>
-- =============================================
Create PROCEDURE [dbo].[AddSubObjectAccess]
	-- Add the parameters for the stored procedure here
	@ObjectID uniqueidentifier,
	@RoleID uniqueidentifier,
	@DetailXML xml=null,
	@CreatedBy nvarchar(250),
	@CreatedDate Datetime,
	@Status int output,
	@ID uniqueidentifier output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	BEGIN TRANSACTION [Tran1]
		BEGIN TRY
		DECLARE @APPID uniqueidentifier=(select AppID From dbo.[Objects] where ID=@ObjectID)
		Delete SObjA from dbo.SubObjectsAccess SObjA Inner Join dbo.[SubObjects] SObj ON SObj.ID=SObjA.SubObjectID 
		LEFT JOIN dbo.[Objects] Obj ON Obj.ID=SObj.ObjectID
		where Obj.AppID=@AppID AND SObjA.RoleID=@RoleID
			set @ID=newid()			
			------------ details-----------
			declare @temp table(
			 
			ID uniqueidentifier,
			SubObjectID uniqueidentifier,
			RoleID uniqueidentifier,			
			[Read] bit,
			Write bit,
			CreatedBy nvarchar,
			CreatedDate Datetime,
			isProcessed bit,
			tmpID uniqueidentifier
			);

			------------parse from xml to temptable ----
			insert into @temp(SubObjectID,RoleID,[Read],Write,CreatedBy,CreatedDate,isProcessed,ID,tmpID)
			select [xmlData].[Col].value('./@SubObjectID', 'uniqueidentifier') as ObjectID,			
			[xmlData].[Col].value('./@RoleID', 'uniqueidentifier') as RoleID,
			[xmlData].[Col].value('./@Read', 'bit') as [Read],
			[xmlData].[Col].value('./@Write', 'bit') as Write,	
			[xmlData].[Col].value('./@CreatedBy', 'nvarchar(20)') as CreatedBy,	
			[xmlData].[Col].value('./@CreatedDate', 'DateTime') as CreatedDate,	
			0,newid(),newid()
			from @DetailXML.nodes('/Details/item') as [xmlData]([Col]);



			---- loop temp and insert each detail row ---------
			declare @loopID uniqueidentifier
			declare @count int
			declare @itemNo int		
			declare @tmpMaterial nvarchar(20)	
			declare @tmpQty int

			select @count=count(*) from @temp where isProcessed=0
			set @itemNo=1

			while(@count>0)
			begin
				--1------------------------------------
				select top 1 @loopID=tmpid from @temp where isProcessed=0

				--2------------------------------------
				insert into [dbo].SubObjectsAccess
				([ID],[SubObjectID],[RoleID],[Read],[Wrie],[CreatedBy],[CreatedDate])
				select ID,SubObjectID,RoleID,[Read],Write,@CreatedBy,@CreatedDate
				from @temp where tmpID=@loopID

				--3-------------------------------------
				--select @tmpQty=isnull(Quantity,0) from @temp where tmpID=@loopID

				
				

				--5-------------------------------------
				update @temp set isProcessed=1  where tmpID=@loopID
				select @count=count(*) from @temp where isProcessed=0
				set @itemNo=@itemNo+1
				
			end

			--4-------- LEDGER ENTRY----------------
			--EXEC [UpdatePaymentLedger] @SCCode,@ID,'ICRIN',@STAmount,@PaymentMode,@CreatedBy

			set @Status=1
			COMMIT TRANSACTION [Tran1]

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION [Tran1]
			DECLARE @ErrorMessage NVARCHAR(4000); 
			select @ErrorMessage=ERROR_MESSAGE()
			RAISERROR( @ErrorMessage,16,1) 
		END CATCH  
END



GO
/****** Object:  StoredProcedure [dbo].[DeleteApplication]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 27-Jun-2017
-- Description:	Delete  Application
-- =============================================
CREATE PROCEDURE [dbo].[DeleteApplication]
	 (
	  @ID uniqueidentifier
	   ,@StatusOut smallint OUTPUT
	 )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM [dbo].[Applications] WHERE ID=@ID  
	 SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END 
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteObject]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Thomson
-- Create date: 27-Jul-2017
-- Description:	<Delete Object>
-- =============================================
Create PROCEDURE [dbo].[DeleteObject]
	-- Add the parameters for the stored procedure here
	@ID uniqueidentifier,
	@AppID uniqueidentifier,
	@Status int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

		BEGIN TRANSACTION [Tran1]
		BEGIN TRY
			Delete from dbo.[Objects]
			Where ID=@ID AND AppID=@AppID
			set @Status=1
			COMMIT TRANSACTION [Tran1]

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION [Tran1]
			DECLARE @ErrorMessage NVARCHAR(4000); 
			select @ErrorMessage=ERROR_MESSAGE()
			RAISERROR( @ErrorMessage,16,1) 
		END CATCH  

	end





GO
/****** Object:  StoredProcedure [dbo].[DeletePrivileges]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 28-Jun-2017
-- Description:	Delete  Application
-- =============================================
CREATE PROCEDURE [dbo].[DeletePrivileges] 
	  @ID uniqueidentifier
	   ,@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM [dbo].[PrivilegesView] WHERE ID=@ID  
	 SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END 
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteRoles]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 27-Jun-2017
-- Description:	Delete  Roles
-- =============================================
CREATE PROCEDURE [dbo].[DeleteRoles] 
	   @ID uniqueidentifier
	   ,@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	  DELETE FROM [dbo].[Roles] WHERE ID=@ID  
	 SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END 
    
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteSubObject]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 30-June-2017
-- Description:	Insert Sub Objects
-- =============================================
CREATE PROCEDURE [dbo].[DeleteSubObject] 
	 	@ID uniqueidentifier, 
	@Status int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   	BEGIN TRANSACTION [Tran1]
		BEGIN TRY
			Delete from dbo.[SubObjects]
			Where ID=@ID 
			set @Status=1
			COMMIT TRANSACTION [Tran1]

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION [Tran1]
			DECLARE @ErrorMessage NVARCHAR(4000); 
			select @ErrorMessage=ERROR_MESSAGE()
			RAISERROR( @ErrorMessage,16,1) 
		END CATCH  
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 23-Jun-2017
-- Description:	Delete User
-- =============================================
CREATE PROCEDURE [dbo].[DeleteUser] 
	 @UserID uniqueidentifier
	   ,@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     DELETE FROM [dbo].[UsersInRoles] WHERE UserID=@UserID 
	 DELETE FROM [dbo].[Users] WHERE ID=@UserID  
	 SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END 



END

GO
/****** Object:  StoredProcedure [dbo].[GetAllApplication]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Thomson K varkey>
-- Create date: <22-06-2017>
-- Description:	<Get all Application>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllApplication]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID,Name,CreatedDate FROM dbo.Applications
	Order by Name Asc
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllHomeLinks]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Thomson>
-- Create date: <21-06-2017>
-- Description:	<Get All Sys_Links Entries>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllHomeLinks] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [ID]
      ,[LinkName]
      ,[LinkDescription]
      ,[Controller]
      ,[Action]
      ,[Type]
	  ,[Order]
     
  FROM [dbo].[Sys_Menu] order by [order]
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllObjectAccess]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Thomson K varkey>
-- Create date: <29-06-2017>
-- Description:	<Get all Objects Access using Application and Role>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllObjectAccess]
	-- Add the parameters for the stored procedure here
	@AppID uniqueidentifier,
	@RoleID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Select ObjA.ID,Obj.ID AS ObjectID,Obj.ObjectName,Obj.AppID,AP.Name AS AppName,R.ID AS RoleID,ISNULL(ObjA.[Read],'False') AS R,ISNULL(ObjA.Write,'False') AS W,ISNULL(ObjA.[Delete],'False') AS D
From dbo.Applications AP
LEFT JOIN dbo.[Objects] Obj ON AP.ID=Obj.AppID
LEFT JOIN dbo.Roles R ON AP.ID=R.AppID
LEFT JOIN dbo.[ObjectsAccess] ObjA 
ON ObjA.ObjectID=Obj.ID AND ObjA.RoleID= @RoleID
Where Obj.AppID=@AppID AND R.ID=@RoleID
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllObjectsOnApplication]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Thomson K varkey>
-- Create date: <22-06-2017>
-- Description:	<Get all Objects respect to application>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllObjectsOnApplication]
	-- Add the parameters for the stored procedure here
	@AppID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.ID,A.ObjectName,A.AppID,B.Name As AppName,A.CreatedDate FROM dbo.[Objects] A
	LEFT JOIN dbo.Applications B On A.AppID=B.ID where A.AppID=@AppID
	Order by Name Asc
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllPrivileges]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 28-Jun-2017
-- Description:	Get All Privileges
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPrivileges] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   
	SELECT  P.ID,A.ID as AppID,R.ID as RoleID,A.Name AS ApplicationName,R.RoleName,P.ModuleName,P.AccessDescription,P.CreatedDate
	from  [dbo].[PrivilegesView] P
	 left join [dbo].[Applications] A ON A.ID=P.AppID
	 left join [dbo].[Roles] R ON r.ID=P.RoleID
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllSubObjectAccess]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Thomson K varkey>
-- Create date: <30-06-2017>
-- Description:	<Get all Sub-Objects Access using Application and Role>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllSubObjectAccess]
	-- Add the parameters for the stored procedure here
	@ObjectID uniqueidentifier,
	@RoleID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @AppID uniqueidentifier=(select AppID From dbo.[Objects] where ID=@ObjectID)
Select SObjA.ID,A.SubObjectID,A.SubObjName,A.ID AS ObjectID,A.ObjectName,A.AppID,AP.Name AS AppName,R.ID AS RoleID,ISNULL(SObjA.[Read],'False') AS R,ISNULL(SObjA.Wrie,'False') AS W
From dbo.Applications AP
LEFT JOIN (select Obj.ID,Obj.ObjectName,Obj.AppID,Sobj.ID AS SubObjectID,Sobj.SubObjName From dbo.[SubObjects] SObj LEFT JOIN dbo.[Objects] Obj ON obj.ID=Sobj.ObjectID where Obj.ID=@ObjectID) AS A
ON AP.ID=A.AppID
LEFT JOIN dbo.Roles R ON AP.ID=R.AppID
LEFT JOIN dbo.[SubObjectsAccess] SObjA 
ON SObjA.SubObjectID=A.SubObjectID AND SObjA.RoleID= @RoleID
Where A.AppID=@AppID AND R.ID=@RoleID
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllSubObjects]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 29-June-2017
-- Description:	GetAllSubObjects
-- =============================================
CREATE PROCEDURE [dbo].[GetAllSubObjects] 
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT SO.[ID],SO.[SubObjName],SO.CreatedDate,
		   O.ObjectName,O.ID AS ObjectID,
		   A.ID AS AppID,A.Name AS AppName 
	FROM  [dbo].[SubObjects] SO
	LEFT JOIN [dbo].[Objects] O ON O.ID=SO.ObjectID
	LEFT JOIN [dbo].[Applications] A ON A.ID=O.AppID
END

GO
/****** Object:  StoredProcedure [dbo].[GetObjectAccess]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Albert Thomson>
-- Create date: <29-06-2017>
-- Description:	<Get User's Object Access Rights>
-- =============================================
CREATE PROCEDURE [dbo].[GetObjectAccess]
	-- Add the parameters for the stored procedure here
	@LoggedName nvarchar(250)
   ,@AppID UNIQUEIDENTIFIER
   ,@ObjectName nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

DECLARE @USERID uniqueidentifier
--Get User ID
SELECT @USERID=ID FROM DBO.Users U WHERE U.LoginName=@LoggedName
DECLARE @OBJECTID uniqueidentifier
--Get Object ID
SELECT @OBJECTID=ID FROM [dbo].[Objects] WHERE ObjectName=@ObjectName AND AppID=@AppID


--Get Access Code for the roles
select AccessRights.ReadRight + AccessRights.WrightRight + AccessRights.DeleteRight + AccessRights.SpecialRight AS UserRight from
(select CASE MAX(CASE [Read] WHEN 'True' THEN 1 WHEN 'False' THEN 0 ELSE 0 END) when 1 then 'R' when 0 then ''end AS ReadRight,
CASE MAX(CASE [Write] WHEN 'True' THEN 1 WHEN 'False' THEN 0 ELSE 0 END) when 1 then 'W' when 0 then ''end AS WrightRight,
CASE MAX(CASE [Delete] WHEN 'True' THEN 1 WHEN 'False' THEN 0 ELSE 0 END) when 1 then 'D' when 0 then ''end AS DeleteRight,
CASE MAX(CASE [Special] WHEN 'True' THEN 1 WHEN 'False' THEN 0 ELSE 0 END) when 1 then 'S' when 0 then ''end AS SpecialRight
 from (select UIR.RoleID,UIR.UserID,R.RoleName from UsersInRoles UIR left join dbo.Roles R on UIR.RoleID=R.ID where UserID=@USERID AND R.AppID=@APPID) AS _Roles 
left join
(select RoleID,ObjectID,[Read],[Write],[Delete],[Special] from dbo.ObjectsAccess where ObjectID=@OBJECTID) AS _ObjectAccess on _Roles.RoleID=_ObjectAccess.RoleID) AS AccessRights


	
END

GO
/****** Object:  StoredProcedure [dbo].[GetRoles]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 22-Jun-2017
-- Description:	Get All Roles 
-- =============================================
CREATE PROCEDURE [dbo].[GetRoles] 
	 @AppID uniqueidentifier=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  R.[ID],R.[RoleName],R.[RoleDescription],A.Name as ApplicationName,A.ID as AppID from [dbo].[Roles] R
	left join [dbo].[Applications] A on R.AppID=A.ID
	where  [AppID]=@AppID  or @AppID is null
END

GO
/****** Object:  StoredProcedure [dbo].[GetSubObjects]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Albert Thomson>
-- Create date: <30-06-2017>
-- Description:	<Get Sub Objects>
-- =============================================
CREATE PROCEDURE [dbo].[GetSubObjects] 
	-- Add the parameters for the stored procedure here
	@LoggedName nvarchar(250)
   ,@AppID UNIQUEIDENTIFIER
   ,@ObjectName nvarchar(250)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

 
DECLARE @USERID uniqueidentifier
SELECT @USERID=ID FROM DBO.Users U WHERE U.LoginName=@LoggedName
DECLARE @OBJECTID uniqueidentifier
SELECT @OBJECTID=ID FROM [dbo].[Objects] WHERE ObjectName=@ObjectName AND AppID=@AppID


--SELECT _SUBOBJECTS.ReadRight + _SUBOBJECTS.WrightRight AS AccessCode,_SUBOBJECTS.SubObjName AS Name FROM(SELECT CASE MAX(CASE SOA.[Read] WHEN 'True' THEN 1 WHEN 'False' THEN 0 ELSE 0 END) when 1 then 'R' when 0 then ''end AS ReadRight,
--CASE MAX(CASE SOA.[Wrie] WHEN 'True' THEN 1 WHEN 'False' THEN 0 ELSE 0 END) when 1 then 'W' when 0 then ''end AS WrightRight,
--SO.SubObjName,SOA.RoleID FROM DBO.SubObjectsAccess AS SOA 
--LEFT JOIN DBO.SubObjects AS SO ON SOA.SubObjectID=SO.ID WHERE SO.ObjectID=@OBJECTID GROUP BY SOA.SubObjectID,SO.SubObjName,SOA.RoleID) AS _SUBOBJECTS

SELECT SO.SubObjName AS Name ,isnull(RSUBS.UserRight,'') AccessCode FROM
DBO.SubObjects SO 
left join
(SELECT _SUBOBJECTS.ReadRight + _SUBOBJECTS.WrightRight AS UserRight,_SUBOBJECTS.SubObjName,SubObjectID FROM(SELECT CASE MAX(CASE SOA.[Read] WHEN 'True' THEN 1 WHEN 'False' THEN 0 ELSE 0 END) when 1 then 'R' when 0 then ''end AS ReadRight,
CASE MAX(CASE SOA.[Wrie] WHEN 'True' THEN 1 WHEN 'False' THEN 0 ELSE 0 END) when 1 then 'W' when 0 then ''end AS WrightRight,SOA.SubObjectID,
SO.SubObjName FROM DBO.SubObjectsAccess AS SOA 
LEFT JOIN DBO.SubObjects AS SO ON SOA.SubObjectID=SO.ID 
INNER JOIN
(select UIR.RoleID,UIR.UserID,R.RoleName from UsersInRoles UIR LEFT JOIN dbo.Roles R on UIR.RoleID=R.ID where UserID=@USERID AND R.AppID=@APPID) AS LROLES
ON LROLES.RoleID=SOA.RoleID
WHERE SO.ObjectID=@OBJECTID GROUP BY SOA.SubObjectID,SO.SubObjName) AS _SUBOBJECTS) AS RSUBS 
on so.ID=RSUBS.SubObjectID where SO.ObjectID=@OBJECTID

 END

GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 22-Jun-2017
-- Description:	Get All User 
-- =============================================
CREATE PROCEDURE [dbo].[GetUser] 
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--SELECT [ID],[LoginName],[UserName],[emailID],[Active],[CreatedDate] 
	--From [dbo].[Users]

	
declare @Roles table ( 
	UserID uniqueidentifier,
	RoleID uniqueidentifier,
	RoleName nvarchar(50)
  )
INSERT into @Roles select a.UserID,a.RoleID,b.RoleName from[dbo].[UsersInRoles] a
inner join [dbo].Roles b on a.RoleID=b.ID  

declare @StuffedRoles table ( 
	UserID uniqueidentifier,	
	RoleList NVARCHAR(MAX),
	RoleListID NVARCHAR(MAX)
  )
 
   

insert into @StuffedRoles SELECT  UserID
       ,STUFF((SELECT ', ' + CAST(RoleName AS VARCHAR(50)) [text()]
         FROM @Roles A
         WHERE UserID = t.UserID
         FOR XML PATH(''), TYPE)
        .value('.','NVARCHAR(MAX)'),1,2,'') List_Output,
		STUFF((SELECT ', ' + CAST(RoleID AS VARCHAR(50)) [text()]
         FROM @Roles A
         WHERE UserID = t.UserID
         FOR XML PATH(''), TYPE)
        .value('.','NVARCHAR(MAX)'),1,2,'') List_Output
FROM @Roles t
GROUP BY UserID


	SELECT U.[ID]
	  ,U.LoginName
      ,U.UserName
	  ,U.emailID
      ,U.[CreatedDate]
   	  ,U.[LoginName]
	  ,U.[Password]
	  ,U.Active
	  ,SR.RoleList
	   ,SR.RoleListID
  FROM [dbo].[Users] AS U
   LEFT JOIN @StuffedRoles AS SR ON U.ID=SR.UserID 
END

GO
/****** Object:  StoredProcedure [dbo].[InsertApplication]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 27-Jun-2017
-- Description:	Insert  Application
-- =============================================
CREATE PROCEDURE [dbo].[InsertApplication]
	 (
	   @Name nvarchar(250)=NULL	 
	,@CreatedBy nvarchar(250)=NULL
	,@CreatedDate smalldatetime=NULL 
	,@ID uniqueidentifier=NULL OUTPUT
    ,@StatusOut smallint OUTPUT
	 )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		IF NOT EXISTS(SELECT ID FROM [dbo].[Applications] where Name=@Name)
	BEGIN
	 SET  @ID=NEWID()
	   INSERT INTO  [dbo].[Applications]
			   ([ID],[Name],[CreatedBy],[CreatedDate])
		 VALUES
			   (@ID,@Name,@CreatedBy,@CreatedDate)

		  SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END
	END
	ELSE
	BEGIN
		SET @StatusOut=2
	END

END



GO
/****** Object:  StoredProcedure [dbo].[InsertObject]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Thomson
-- Create date: 22-Jul-2017
-- Description:	<Insert Object>
-- =============================================
Create PROCEDURE [dbo].[InsertObject]
	-- Add the parameters for the stored procedure here
	@AppID uniqueidentifier,
	@ObjectName nvarchar(250),
	@CreatedBy nvarchar(250)=null,
	@CreatedDate datetime=null ,
	@Status int output,
	@ID uniqueidentifier output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if exists(select * from [Objects] where [ObjectName]=@ObjectName)
	begin
		RAISERROR('INVE01',16,1) 
	end
	else
	begin

		BEGIN TRANSACTION [Tran1]
		BEGIN TRY
			set @ID=newid()
			insert into dbo.[Objects](ID,ObjectName,AppID,CreatedBy,CreatedDate)
			values(@ID,@ObjectName,@AppID,@CreatedBy,@CreatedDate)

			set @Status=1
			COMMIT TRANSACTION [Tran1]

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION [Tran1]
			DECLARE @ErrorMessage NVARCHAR(4000); 
			select @ErrorMessage=ERROR_MESSAGE()
			RAISERROR( @ErrorMessage,16,1) 
		END CATCH  

	end


	end





GO
/****** Object:  StoredProcedure [dbo].[InsertPrivileges]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 28-Jun-2017
-- Description:	Insert Privileges
-- =============================================
CREATE PROCEDURE [dbo].[InsertPrivileges] 
	 
	@AppID  uniqueidentifier
	,@RoleID uniqueidentifier
	,@ModuleName nvarchar(250)=null
	,@AccessDescription nvarchar(max)=NULL
	,@CreatedBy nvarchar(250)=NULL
	,@CreatedDate smalldatetime=NULL 
	,@ID uniqueidentifier=NULL OUTPUT
    ,@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	 SET  @ID=NEWID()
	   INSERT INTO  [dbo].[PrivilegesView]
			   ([ID],[AppID],[RoleID],[ModuleName],[AccessDescription],[CreatedBy],[CreatedDate])
		 VALUES
			   (@ID,@AppID,@RoleID,@ModuleName,@AccessDescription,@CreatedBy,@CreatedDate)

		  SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END
    
END

GO
/****** Object:  StoredProcedure [dbo].[InsertRoles]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 27-Jun-2017
-- Description:	Insert  Roles
-- =============================================
CREATE PROCEDURE  [dbo].[InsertRoles]
	  (
	   @RoleName nvarchar(250) 
	   ,@AppID uniqueidentifier
	   ,@RoleDescription nvarchar(250)
	,@CreatedBy nvarchar(250)=NULL
	,@CreatedDate smalldatetime=NULL 
	,@ID uniqueidentifier=NULL OUTPUT
    ,@StatusOut smallint OUTPUT
	 )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  	IF NOT EXISTS(SELECT ID FROM [dbo].[Roles] where RoleName=@RoleName and AppID=@AppID)
	BEGIN
	 SET  @ID=NEWID()
	   INSERT INTO  [dbo].[Roles]
			   ([ID],[RoleName],[AppID],[RoleDescription],[CreatedBy],[CreatedDate])
		 VALUES
			   (@ID,@RoleName,@AppID,@RoleDescription,@CreatedBy,@CreatedDate)

		  SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END
	END
	ELSE
	BEGIN
		SET @StatusOut=2
	END
END

GO
/****** Object:  StoredProcedure [dbo].[InsertSubObjects]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 29-June-2017
-- Description:	Insert Sub Objects
-- =============================================
CREATE PROCEDURE [dbo].[InsertSubObjects]
 	@ObjectID uniqueidentifier,
	@SubObjName nvarchar(250),
	@CreatedBy nvarchar(250)=null,
	@CreatedDate datetime=null ,
	@Status int output,
	@ID uniqueidentifier output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  if exists(select * from [SubObjects] where [SubObjName]=@SubObjName and  [ObjectID]=@ObjectID)
	begin
		RAISERROR('INVE01',16,1) 
	end
	else
	begin

		BEGIN TRANSACTION [Tran1]
		BEGIN TRY
			set @ID=newid()
			insert into dbo.[SubObjects](ID,SubObjName,ObjectID,CreatedBy,CreatedDate)
			values(@ID,@SubObjName,@ObjectID,@CreatedBy,@CreatedDate)

			set @Status=1
			COMMIT TRANSACTION [Tran1]

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION [Tran1]
			DECLARE @ErrorMessage NVARCHAR(4000); 
			select @ErrorMessage=ERROR_MESSAGE()
			RAISERROR( @ErrorMessage,16,1) 
		END CATCH  

	end
END

GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 21-Jun-2017
-- Description:	Insert  User
-- =============================================
CREATE PROCEDURE [dbo].[InsertUser]
	  @UserName nvarchar(250)=NULL
	,@RoleList nvarchar(MAX)=NULL
	,@LoginName nvarchar(250)
	,@EmailID nvarchar(250)=null
	,@Password nvarchar(250)=NULL
	,@CreatedBy nvarchar(250)=NULL
	,@CreatedDate smalldatetime=NULL
	,@Active bit
	,@UserID uniqueidentifier=NULL OUTPUT
    ,@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT ID FROM [dbo].[Users] where LoginName=@LoginName)
BEGIN
 SET  @UserID=NEWID()
	INSERT INTO [dbo].[Users]
           ([ID],[UserName],[LoginName],[emailID],[Password],[Active]
           ,[CreatedBy],[CreatedDate])
     VALUES
           (@UserID,@UserName,@LoginName,@EmailID,@Password,@Active
           ,@CreatedBy,@CreatedDate)

      SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END
	  if(@StatusOut>0)
	  begin
	  WHILE len(@RoleList) > 0
	  BEGIN
		  INSERT INTO [dbo].[UsersInRoles]
			   ([ID],[UserID],[RoleID],[CreatedBy],[CreatedDate])
			VALUES
					   --LEFT ( character_expression , integer_expression ) --CHARINDEX( substring, string, [start_position] )
			   (newid(),@UserID,left(@RoleList, charindex(',', @RoleList+',')-1),@CreatedBy,@CreatedDate)

			  -- STUFF ( character_expression , start , length , replaceWith_expression )  
		  SET @RoleList = stuff(@RoleList, 1, charindex(',', @RoleList+','), '')

	  END
	  end
END
ELSE
BEGIN
SET @StatusOut=2
END
   



END

GO
/****** Object:  StoredProcedure [dbo].[UpdateApplication]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 27-Jun-2017
-- Description:	Update  Application
-- =============================================
CREATE PROCEDURE [dbo].[UpdateApplication]
	   @Name nvarchar(250)=NULL	 
	,@UpdatedBy nvarchar(250)=NULL
	,@UpdatedDate smalldatetime=NULL 
	,@ID uniqueidentifier 
    ,@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF NOT EXISTS(SELECT ID FROM [dbo].[Applications] where Name=@Name and  [ID]!=@ID)
	BEGIN
		UPDATE [dbo].[Applications] 
		SET [Name]=@Name,
		[UpdatedBy]=@UpdatedBy,
		[UpdatedDate]=@UpdatedDate

		WHERE [ID]=@ID
		  SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END
	END
	ELSE
	BEGIN
		SET @StatusOut=2
	END


    
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateObject]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Thomson
-- Create date: 22-Jul-2017
-- Description:	<Update Object>
-- =============================================
Create PROCEDURE [dbo].[UpdateObject]
	-- Add the parameters for the stored procedure here
	@ID uniqueidentifier,
	@AppID uniqueidentifier,
	@ObjectName nvarchar(250),
	@UpdatedBy nvarchar(250)=null,
	@UpdatedDate datetime=null ,
	@Status int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

		BEGIN TRANSACTION [Tran1]
		BEGIN TRY
			Update dbo.[Objects] SET ObjectName=@ObjectName,AppID=@AppID,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate
			Where ID=@ID 
			set @Status=1
			COMMIT TRANSACTION [Tran1]

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION [Tran1]
			DECLARE @ErrorMessage NVARCHAR(4000); 
			select @ErrorMessage=ERROR_MESSAGE()
			RAISERROR( @ErrorMessage,16,1) 
		END CATCH  

	end





GO
/****** Object:  StoredProcedure [dbo].[UpdatePrivileges]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 28-Jun-2017
-- Description:	Update Privileges
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePrivileges]
	@AppID  uniqueidentifier
	,@RoleID uniqueidentifier
	,@ModuleName nvarchar(250)=null
	,@AccessDescription nvarchar(max)=NULL
	,@UpdatedBy nvarchar(250)=NULL
	,@UpdatedDate smalldatetime=NULL 
	,@ID uniqueidentifier 
    ,@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE [dbo].[PrivilegesView] 
		SET  
		[AppID]=@AppID,
		[RoleID]=@RoleID,
		[ModuleName]=@ModuleName,
		[AccessDescription]=@AccessDescription,
		[UpdatedBy]=@UpdatedBy,
		[UpdatedDate]=@UpdatedDate
		WHERE [ID]=@ID

		SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END
  
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateRoles]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 27-Jun-2017
-- Description:	Update  Roles
-- =============================================
CREATE PROCEDURE  [dbo].[UpdateRoles]
	@ID uniqueidentifier,
    @RoleName nvarchar(250),
	@AppID uniqueidentifier,
	@RoleDescription nvarchar(250),
	@UpdatedBy nvarchar(250)=NULL,
	@UpdatedDate smalldatetime=NULL,
	@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		IF NOT EXISTS(SELECT ID FROM [dbo].[Roles] where RoleName=@RoleName and AppID=@AppID and  [ID]!=@ID)
	BEGIN
	
	   update [dbo].[Roles]
			  SET  
			  [RoleName]=@RoleName,
			  [AppID]=@AppID,
			  [RoleDescription]=@RoleDescription,
			  [UpdatedBy]=@UpdatedBy,
			  [UpdatedDate]=@UpdatedDate    WHERE [ID]=@ID
	
	 SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END
	
	END
	ELSE
	BEGIN
		SET @StatusOut=2
	END
  
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateSubObjects]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 30-June-2017
-- Description:	Insert Sub Objects
-- =============================================
CREATE PROCEDURE [dbo].[UpdateSubObjects]
	-- Add the parameters for the stored procedure here
	@ID uniqueidentifier,
 	@ObjectID uniqueidentifier,
	@SubObjName nvarchar(250),
	@UpdatedBy nvarchar(250)=null,
	@UpdatedDate datetime=null ,
	@Status int output
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON; 
    

if exists(select * from [SubObjects] where [SubObjName]=@SubObjName and [ID]<>@ID and [ObjectID]=@ObjectID)
	begin
		RAISERROR('INVE01',16,1) 
	end
	else
	begin


		BEGIN TRANSACTION [Tran1]
		
		BEGIN TRY
		
		update [dbo].[SubObjects] 
		set [SubObjName]=@SubObjName,
			[ObjectID]=@ObjectID,
			[UpdatedBy]=@UpdatedBy,
			[UpdatedDate]=@UpdatedDate
		where [ID]=@ID
		set @Status=1
		
		COMMIT TRANSACTION [Tran1]

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION [Tran1]
			DECLARE @ErrorMessage NVARCHAR(4000); 
			select @ErrorMessage=ERROR_MESSAGE()
			RAISERROR( @ErrorMessage,16,1) 
		END CATCH  
	end
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 03-Jul-2017 11:47:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gibin Jacob Job
-- Create date: 23-Jun-2017
-- Description:	Update User
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser]
	 @UserID uniqueidentifier
	,@UserName nvarchar(250)=NULL
	,@RoleList nvarchar(MAX)=NULL
	,@LoginName nvarchar(250)
	,@EmailID nvarchar(250)=null
	,@Password nvarchar(250)=NULL
	,@UpdatedBy nvarchar(250)=NULL
	,@UpdatedDate smalldatetime=NULL
	,@Active bit 
    ,@StatusOut smallint OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  IF NOT EXISTS(SELECT ID FROM [dbo].[Users] where LoginName=@LoginName and [ID]!=@UserID )
BEGIN
 
 	DECLARE @SAdmID uniqueidentifier
	UPDATE [dbo].[Users]
    SET [UserName]=@UserName,[LoginName]=@LoginName,
		[emailID]=@EmailID,[Active]=@Active,
		[Password]=IsNull(@Password,[Password]),
		[UpdatedBy]=@UpdatedBy,[UpdatedDate]=@UpdatedDate
    WHERE [ID]=@UserID

    SET @StatusOut = CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END
	  if(@StatusOut>0)
	  begin
	  
	  select @SAdmID =ID from Roles where  RoleName='SAdmin'

	  DELETE FROM [dbo].[UsersInRoles] WHERE UserID=@UserID and (RoleID <>@SAdmID OR @SAdmID IS NULL)

	  WHILE len(@RoleList) > 0
	  BEGIN
		  INSERT INTO [dbo].[UsersInRoles]
			   ([ID],[UserID],[RoleID],[CreatedBy],[CreatedDate])
			VALUES
					   --LEFT ( character_expression , integer_expression ) --CHARINDEX( substring, string, [start_position] )
			   (newid(),@UserID,left(@RoleList, charindex(',', @RoleList+',')-1),@UpdatedBy,@UpdatedDate)

			  -- STUFF ( character_expression , start , length , replaceWith_expression )  
		  SET @RoleList = stuff(@RoleList, 1, charindex(',', @RoleList+','), '')

	  END
	  end
END
ELSE
BEGIN
SET @StatusOut=2
END
   

END

GO


---User with SAdmin Role creation-------
USE [SPApps]
--Application Creation
DECLARE @AppID uniqueidentifier
SET @AppID='D8503173-99F9-45CC-BE2A-81340BD13E25' --Change this id for new project
INSERT INTO [dbo].[Applications]([ID],[Name],[CreatedBy],[CreatedDate])
VALUES  (@AppID,'SPAccounts','SAdmin',getdate())


--ROLE CREATION

DECLARE @RoleID uniqueidentifier
set @RoleID='2df7f0a3-152a-4b86-9b2e-77daa59c1638'
INSERT INTO [dbo].[Roles] ([ID],[AppID],[RoleName],[RoleDescription],[CreatedBy],[CreatedDate])
VALUES(@RoleID,@AppID,'SAdmin','SAdmin Main Administrator','SAdmin',getdate())

--USER CREATION
DECLARE @UserID uniqueidentifier
set @UserID='0f6318c2-feec-4487-a1bd-81b27ee6226a'
INSERT INTO [dbo].[Users]([ID],[LoginName],[UserName],[emailID],[Active],[Password],[CreatedBy],[CreatedDate])
VALUES (@UserID,'suvaneeth','suvaneeth s','suvaneeth@gmail.com','True','QjwzLuKDyqoArNCPG0kFiQ==','SAdmin',getdate())

--Entry in USER IN ROLES 
INSERT INTO [dbo].[UsersInRoles]([ID],[RoleID],[UserID],[CreatedBy],[CreatedDate])
VALUES(NEWID(),@RoleID,@UserID,'SAdmin',getdate())



--Sys Menu items
use [SPApps]
INSERT INTO [dbo].[Sys_Menu]([ID],[LinkName],[LinkDescription],[Type],[Controller],[Action],[Order])
VALUES (NEWID(),'Define Application','This is a dummy text for Define Application','RHS','Application','Index' ,0)

INSERT INTO [dbo].[Sys_Menu]([ID],[LinkName],[LinkDescription],[Type],[Controller],[Action],[Order])
VALUES (NEWID(),'Manage Users','Use the Manage users page to view a list of users you previously created, select the option to edit a user'+'s profile, or select the option to create a new user.','LHS','User','Index' ,0)

INSERT INTO [dbo].[Sys_Menu]([ID],[LinkName],[LinkDescription],[Type],[Controller],[Action],[Order])
VALUES (NEWID(),'View Privileges','Application privileges can be assigned to an individual user or to a group of users, for example, in a user role. The user role can also be used to assign system, object, package etc..','LHS','Privileges','PrivilegesView' ,1)

INSERT INTO [dbo].[Sys_Menu]([ID],[LinkName],[LinkDescription],[Type],[Controller],[Action],[Order])
VALUES (NEWID(),'Define Objects','Create / Edit objects under applications......','RHS','AppObject',NULL ,1)

INSERT INTO [dbo].[Sys_Menu]([ID],[LinkName],[LinkDescription],[Type],[Controller],[Action],[Order])
VALUES (NEWID(),'Define Roles','This is a dummy text for Define Roles','RHS','Roles','Index' ,2)

INSERT INTO [dbo].[Sys_Menu]([ID],[LinkName],[LinkDescription],[Type],[Controller],[Action],[Order])
VALUES (NEWID(),'Manage Access','Create / Delete Access to the object and its sub objects','RHS','ManageAccess','Index' ,3)

INSERT INTO [dbo].[Sys_Menu]([ID],[LinkName],[LinkDescription],[Type],[Controller],[Action],[Order])
VALUES (NEWID(),'Manage View Privileges','This is a dummy text for Manage '+'View Privileges','RHS','Privileges','Index' ,4)