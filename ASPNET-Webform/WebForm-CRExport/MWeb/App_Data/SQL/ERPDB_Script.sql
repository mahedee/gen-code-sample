USE [ERPDB]
GO
/****** Object:  StoredProcedure [dbo].[rpt_get_employeeinfo_by_designation_id]    Script Date: 08/01/2016 10:24:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- EXEC rpt_get_employeeinfo_by_designation_id 0
-- =============================================
CREATE PROCEDURE [dbo].[rpt_get_employeeinfo_by_designation_id]
	@designation int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT emp.[EmpCode]
           ,emp.[FullName]
           ,emp.[DesignationID]
		   ,dgn.DesignationName
           ,emp.[FatherName]
           ,emp.[MotherName]
           ,emp.[BloodGroup]
           ,emp.[DOB]
     FROM Employee emp
	 INNER JOIN Designation dgn
	 ON emp.DesignationID = dgn.Id
	 WHERE emp.DesignationID = @designation OR @designation = 0

END

GO
/****** Object:  Table [dbo].[Designation]    Script Date: 08/01/2016 10:24:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Designation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [varchar](150) NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 08/01/2016 10:24:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpCode] [varchar](50) NULL,
	[FullName] [varchar](150) NULL,
	[DesignationID] [int] NOT NULL,
	[FatherName] [varchar](150) NULL,
	[MotherName] [varchar](150) NULL,
	[BloodGroup] [varchar](4) NULL,
	[DOB] [datetime] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Designation] ON 

GO
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (1, N'Software Engineer')
GO
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (2, N'Senior Software Engineer')
GO
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (3, N'Software Architect')
GO
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (4, N'Business Analyst')
GO
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (5, N'Project Manager')
GO
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (6, N'Product Manager')
GO
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (7, N'Manager')
GO
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (8, N'AGM')
GO
SET IDENTITY_INSERT [dbo].[Designation] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

GO
INSERT [dbo].[Employee] ([Id], [EmpCode], [FullName], [DesignationID], [FatherName], [MotherName], [BloodGroup], [DOB]) VALUES (1, N'SSD001', N'Md. Mahedee Hasan', 3, N'Easin Bhuiyan', N'Moriam Begum', N'A+', CAST(0x00007AA600000000 AS DateTime))
GO
INSERT [dbo].[Employee] ([Id], [EmpCode], [FullName], [DesignationID], [FatherName], [MotherName], [BloodGroup], [DOB]) VALUES (2, N'SSD002', N'Anamul Haque', 3, N'Not Provided', N'Not Provided', N'B+', CAST(0x0000766B00000000 AS DateTime))
GO
INSERT [dbo].[Employee] ([Id], [EmpCode], [FullName], [DesignationID], [FatherName], [MotherName], [BloodGroup], [DOB]) VALUES (3, N'SSD003', N'Sayeda Rusdana Sarwat Esha', 1, N'Not Provided', N'Not Provided', N'B+', CAST(0x0000806800000000 AS DateTime))
GO
INSERT [dbo].[Employee] ([Id], [EmpCode], [FullName], [DesignationID], [FatherName], [MotherName], [BloodGroup], [DOB]) VALUES (4, N'SSD004', N'Moinul Islam', 1, N'Not Provided', N'Not Provided', N'O+', CAST(0x00007D8D00000000 AS DateTime))
GO
INSERT [dbo].[Employee] ([Id], [EmpCode], [FullName], [DesignationID], [FatherName], [MotherName], [BloodGroup], [DOB]) VALUES (5, N'SSD005', N'Md. Rezowan Hossain Suvro', 1, N'Not Provided', N'Not Provided', N'AB+', CAST(0x00007D8D00000000 AS DateTime))
GO
INSERT [dbo].[Employee] ([Id], [EmpCode], [FullName], [DesignationID], [FatherName], [MotherName], [BloodGroup], [DOB]) VALUES (6, N'SSD006', N'Salma Sultana', 1, N'Not Provided', N'Not Provided', N'B+', CAST(0x0000806800000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([DesignationID])
REFERENCES [dbo].[Designation] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO
