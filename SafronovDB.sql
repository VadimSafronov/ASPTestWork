USE [DBName]
GO
/****** Object:  Table [dbo].[tb_Company]    Script Date: 29.11.2020 23:57:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Company](
	[CompanyID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[CompType] [nvarchar](64) NOT NULL,
	[Workers] [int] NULL,
 CONSTRAINT [PK_tb_Person] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Person]    Script Date: 29.11.2020 23:57:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Surname] [nvarchar](64) NOT NULL,
	[Patronymic] [nvarchar](64) NOT NULL,
	[StartDate] [date] NOT NULL,
	[Position] [int] NOT NULL,
	[Company] [int] NOT NULL,
 CONSTRAINT [PK_tb_Company] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Position]    Script Date: 29.11.2020 23:57:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Position](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_tb_Status] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_Person]  WITH CHECK ADD  CONSTRAINT [FK_tb_Person_tb_Company] FOREIGN KEY([Company])
REFERENCES [dbo].[tb_Company] ([CompanyID])
GO
ALTER TABLE [dbo].[tb_Person] CHECK CONSTRAINT [FK_tb_Person_tb_Company]
GO
ALTER TABLE [dbo].[tb_Person]  WITH CHECK ADD  CONSTRAINT [FK_tb_Person_tb_Position] FOREIGN KEY([Position])
REFERENCES [dbo].[tb_Position] ([PositionID])
GO
ALTER TABLE [dbo].[tb_Person] CHECK CONSTRAINT [FK_tb_Person_tb_Position]
GO
/****** Object:  StoredProcedure [dbo].[pr_AddCompany]    Script Date: 29.11.2020 23:57:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pr_AddCompany] 
						@title nvarchar(64),
						@comptype nvarchar(64),
						@workers int
						
AS
BEGIN
	INSERT INTO tb_Company values (@title,@comptype,@workers)
END
GO
/****** Object:  StoredProcedure [dbo].[pr_AddPerson]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_AddPerson] 
						@name nvarchar(64),
						@surname nvarchar(64),
						@patronymic nvarchar(64),
						@startdate date,
						@position int, 
						@company int
AS
BEGIN
	INSERT INTO tb_Person values (@name, @surname,@patronymic, @startdate, @position, @company)
	
END
GO
/****** Object:  StoredProcedure [dbo].[pr_DeleteCompany]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pr_DeleteCompany]
						@id int

AS
BEGIN
UPDATE tb_Person SET Company = (SELECT TOP 1 [CompanyID] FROM tb_Company WHERE CompanyID <> @id) WHERE Company = @id;
	Delete FROM tb_Company WHERE CompanyID = @id
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[pr_DeletePerson]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DeletePerson]
						@id int

AS
BEGIN
	Delete FROM tb_Person WHERE PersonID=@id
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllCompanies]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_SelectAllCompanies] 

AS
BEGIN
    UPDATE tb_Company SET Workers = (  SELECT COUNT(*)  FROM tb_Person WHERE tb_Company.CompanyID = tb_Person.Company );
	SELECT * FROM tb_Company 
	
END
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPeople]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPeople] 			
AS
BEGIN
	SELECT * FROM tb_Person 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCompany]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCompany] 
						@id int
AS
BEGIN
	SELECT * FROM tb_Company WHERE CompanyID = @id
END
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectPerson]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectPerson] 
						@id int
AS
BEGIN
	SELECT * FROM tb_Person WHERE PersonID = @id
END
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectPosition]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectPosition] 

AS
BEGIN
	SELECT * FROM tb_Position
END
GO
/****** Object:  StoredProcedure [dbo].[tb_UpdateCompany]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tb_UpdateCompany] 
						@id int,
						@title nvarchar(64),
						@comptype nvarchar(64)
						
AS
BEGIN
	UPDATE tb_Company SET Title=@title, CompType=@comptype where CompanyID=@id
END
GO
/****** Object:  StoredProcedure [dbo].[tb_UpdatePerson]    Script Date: 29.11.2020 23:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tb_UpdatePerson] 
						@id int,
						@name nvarchar(64),
						@surname nvarchar(64),
						@patronymic nvarchar(64),
						@startdate date,
						@position int,
						@company int

AS
BEGIN
	UPDATE tb_Person SET Name=@name, Surname=@surname,Patronymic = @patronymic ,StartDate =@startdate,Position=@position,Company=@company where PersonID=@id
	
END
GO
USE [master]
GO
ALTER DATABASE [SafronovTest] SET  READ_WRITE 
GO
