
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/16/2022 11:50:41
-- Generated from EDMX file: C:\Users\isoto\OneDrive - Institute of Technology Sligo\OOSD2 - Ignacio Benito\Week 13 - Assessments - CA3\CA3_s00220273\GuitarBookingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CA3_s00220273_Data];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Guitars'
CREATE TABLE [dbo].[Guitars] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Make] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [Color] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [GuitarId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Guitars'
ALTER TABLE [dbo].[Guitars]
ADD CONSTRAINT [PK_Guitars]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GuitarId] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_GuitarBooking]
    FOREIGN KEY ([GuitarId])
    REFERENCES [dbo].[Guitars]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GuitarBooking'
CREATE INDEX [IX_FK_GuitarBooking]
ON [dbo].[Bookings]
    ([GuitarId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------