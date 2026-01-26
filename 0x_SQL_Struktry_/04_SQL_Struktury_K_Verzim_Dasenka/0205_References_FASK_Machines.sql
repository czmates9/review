/****** Object:  ForeignKey [FK_FASK_Machines_FASK_MachineType]   ******/

ALTER TABLE [FASK_Machines]  WITH CHECK ADD  CONSTRAINT [FK_FASK_Machines_FASK_MachineType] FOREIGN KEY([machinetype])
REFERENCES [FASK_MachineType] ([machinetype])
GO
ALTER TABLE [FASK_Machines] CHECK CONSTRAINT [FK_FASK_Machines_FASK_MachineType]
GO

/**************************************************************************************/