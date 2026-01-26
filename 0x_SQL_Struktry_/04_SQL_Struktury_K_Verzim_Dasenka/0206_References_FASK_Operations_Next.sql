/****** Object:  ForeignKey [FK_FASK_Operations_Next_FASK_Operations]    ******/
ALTER TABLE [FASK_Operations_Next]  WITH CHECK ADD  CONSTRAINT [FK_FASK_Operations_Next_FASK_Operations] FOREIGN KEY([machinetype], [IDO])
REFERENCES [FASK_Operations] ([machinetype], [IDO])
GO
ALTER TABLE [FASK_Operations_Next] CHECK CONSTRAINT [FK_FASK_Operations_Next_FASK_Operations]
GO


/********************************************************************************************************/

/****** Object:  ForeignKey [FK_FASK_Operations_Next_FASK_Operations1]   ******/
ALTER TABLE [FASK_Operations_Next]  WITH CHECK ADD  CONSTRAINT [FK_FASK_Operations_Next_FASK_Operations1] FOREIGN KEY([machinetype], [IDO_NEXT])
REFERENCES [FASK_Operations] ([machinetype], [IDO])
GO
ALTER TABLE [FASK_Operations_Next] CHECK CONSTRAINT [FK_FASK_Operations_Next_FASK_Operations1]
GO

/********************************************************************************************************/