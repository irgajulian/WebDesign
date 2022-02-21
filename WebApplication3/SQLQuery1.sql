 UPDATE dbo.shimano SET roomTemp1 = '17.4',roomTemp2 = '18.4',machineTemp1 = '32.8',machineTemp2 = '37.5', roomHumid1 = '17.4',roomHumid2 = '18.4',machineHumid1 = '32.8',machineHumid2 = '37.5', Time = '1' WHERE ID = 1
--DELETE FROM dbo.shimano WHERE romtemp = NULL
--CREATE TABLE dbo.a_a ([romtemp1] TEXT NULL,[romtemp] TEXT NULL);
--SELECT CASE WHEN OBJECT_ID('dbo.shimano', 'U') IS NOT NULL THEN 'true' ELSE 'false' END
--INSERT INTO	dbo.shimano (roomTemp1, roomTemp2, machineTemp1, machineTemp2, roomHumid1, roomHumid2, machineHumid1, machineHumid2) VALUES ('10', '10', '10', '10', '10', '10', '10', '10')	