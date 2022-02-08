CREATE TABLE [dbo].[shimano] (
    [romtemp1]     TEXT NULL,
    [romtemp]      TEXT NULL,
    [machinetemp1] TEXT NULL,
    [machinetemp2] TEXT NULL,
    [time]         INT		IDENTITY (1,1)	NOT NULL,
	PRIMARY KEY CLUSTERED ([time] ASC)
);

