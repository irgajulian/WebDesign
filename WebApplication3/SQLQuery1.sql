IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = 'SHIMANODB')
BEGIN
CREATE DATABASE SHIMANODB;
END
GO

USE SHIMANODB;
GO

CREATE TABLE [dbo].[Config] (
    [ID]             INT  IDENTITY (1, 1) NOT NULL,
    [upperlimit]    TEXT NULL,
    [lowerlimit]     TEXT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[shimano] (
    [ID]            INT  IDENTITY (1, 1) NOT NULL,
    [roomTemp1]     TEXT NULL,
    [roomTemp2]     TEXT NULL,
    [machineTemp1]  TEXT NULL,
    [machineTemp2]  TEXT NULL,
    [roomHumid1]    TEXT NULL,
    [roomHumid2]    TEXT NULL,
    [machineHumid1] TEXT NULL,
    [machineHumid2] TEXT NULL,
    [time]          TEXT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[login] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [username] VARCHAR (50) NULL,
    [password] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

INSERT INTO dbo.login (username,password) VALUES ('admin','admin');
INSERT INTO dbo.Config(upperlimit,lowerlimit) VALUES ('40','20');
INSERT INTO	dbo.shimano (roomTemp1, roomTemp2, machineTemp1, machineTemp2, roomHumid1, roomHumid2, machineHumid1, machineHumid2) VALUES ('0', '0', '0', '0', '0', '0', '0', '0')
GO