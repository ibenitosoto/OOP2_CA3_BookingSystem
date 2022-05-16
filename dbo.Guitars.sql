CREATE TABLE [dbo].[Guitars] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Brand]  NVARCHAR (MAX) NOT NULL,
    [Model] NVARCHAR (MAX) NOT NULL,
    [Color] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Guitars] PRIMARY KEY CLUSTERED ([Id] ASC)
);

