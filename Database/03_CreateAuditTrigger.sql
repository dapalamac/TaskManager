IF OBJECT_ID(N'dbo.TaskAudit', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TaskAudit
    (
        AuditId int IDENTITY(1, 1) NOT NULL,
        TaskId int NOT NULL,
        OldStatus int NOT NULL,
        NewStatus int NOT NULL,
        ChangedAt datetime2 NOT NULL,
        ChangedByUserId int NOT NULL,
        CONSTRAINT PK_TaskAudit PRIMARY KEY (AuditId),
        CONSTRAINT FK_TaskAudit_Tasks_TaskId
            FOREIGN KEY (TaskId) REFERENCES dbo.Tasks (Id),
        CONSTRAINT FK_TaskAudit_Users_ChangedByUserId
            FOREIGN KEY (ChangedByUserId) REFERENCES dbo.Users (Id)
    );
END;
GO

IF OBJECT_ID(N'dbo.trg_Tasks_StatusAudit', N'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_Tasks_StatusAudit;
END;
GO

CREATE TRIGGER dbo.trg_Tasks_StatusAudit
ON dbo.Tasks
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TaskAudit
    (
        TaskId,
        OldStatus,
        NewStatus,
        ChangedAt,
        ChangedByUserId
    )
    SELECT
        inserted.Id,
        deleted.Status,
        inserted.Status,
        SYSUTCDATETIME(),
        inserted.UserId
    FROM inserted
    INNER JOIN deleted ON deleted.Id = inserted.Id
    WHERE inserted.Status <> deleted.Status;
END;
GO
