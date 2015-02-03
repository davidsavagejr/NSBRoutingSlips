/****** Object:  Table [dbo].[Component_A]    Script Date: 2/3/2015 8:40:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Component_A](
	[id] [uniqueidentifier] NOT NULL,
	[batchid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ComponentA] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Component_B]    Script Date: 2/3/2015 8:40:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Component_B](
	[id] [uniqueidentifier] NOT NULL,
	[batchid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ComponentB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Widget]    Script Date: 2/3/2015 8:40:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Widget](
	[id] [uniqueidentifier] NOT NULL,
	[component_a_id] [uniqueidentifier] NULL,
	[component_b_id] [uniqueidentifier] NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Widget] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Widget]  WITH NOCHECK ADD  CONSTRAINT [FK_Widget_ComponentA] FOREIGN KEY([component_a_id])
REFERENCES [dbo].[Component_A] ([id])
GO
ALTER TABLE [dbo].[Widget] CHECK CONSTRAINT [FK_Widget_ComponentA]
GO
ALTER TABLE [dbo].[Widget]  WITH NOCHECK ADD  CONSTRAINT [FK_Widget_ComponentB] FOREIGN KEY([component_b_id])
REFERENCES [dbo].[Component_B] ([id])
GO
ALTER TABLE [dbo].[Widget] CHECK CONSTRAINT [FK_Widget_ComponentB]
GO
