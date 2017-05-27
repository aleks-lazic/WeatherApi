
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/27/2017 14:25:20
-- Generated from EDMX file: C:\Users\aleks\Source\Repos\WeatherApi\WeatherAPI\WeatherAPI\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database1];
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

-- Creating table 'Weather'
CREATE TABLE [dbo].[Weather] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [degreeMorning] int  NOT NULL,
    [degreeAfternoon] int  NOT NULL,
    [precipitation] int  NOT NULL,
    [humidity] int  NOT NULL,
    [wind] int  NOT NULL,
    [date] datetime  NOT NULL,
    [City_Id] int  NOT NULL
);
GO

-- Creating table 'City'
CREATE TABLE [dbo].[City] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Weather'
ALTER TABLE [dbo].[Weather]
ADD CONSTRAINT [PK_Weather]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'City'
ALTER TABLE [dbo].[City]
ADD CONSTRAINT [PK_City]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [City_Id] in table 'Weather'
ALTER TABLE [dbo].[Weather]
ADD CONSTRAINT [FK_CityWeather]
    FOREIGN KEY ([City_Id])
    REFERENCES [dbo].[City]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityWeather'
CREATE INDEX [IX_FK_CityWeather]
ON [dbo].[Weather]
    ([City_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------