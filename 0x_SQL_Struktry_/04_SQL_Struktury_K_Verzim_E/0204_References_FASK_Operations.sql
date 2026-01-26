/****** Object:  ForeignKey [FK_FASK_Operations_FASK_MachineType]    ******/

ALTER TABLE [FASK_Operations]  WITH CHECK ADD  CONSTRAINT [FK_FASK_Operations_FASK_MachineType] FOREIGN KEY([machinetype])
REFERENCES [FASK_MachineType] ([machinetype])
GO
ALTER TABLE [FASK_Operations] CHECK CONSTRAINT [FK_FASK_Operations_FASK_MachineType]
GO

/********************************************************************************************************/