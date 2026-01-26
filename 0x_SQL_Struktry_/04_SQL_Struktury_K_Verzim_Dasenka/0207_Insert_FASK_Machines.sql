/***** FAKE machine, co se nepouživa, ale prostě musí byt ***********************************************/

INSERT INTO [dbo].[FASK_MachineType] ([machinetype] ,[machinetypename]) VALUES ('1','stroj')


INSERT INTO [dbo].[FASK_Machines] ([id], [machinetype] ,[name] ,[description], [koeficient]) VALUES (N'1', '1', N'1', N'1', 0)

/********************************************************************************************************/