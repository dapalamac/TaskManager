IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Tasks_UserId'
      AND object_id = OBJECT_ID(N'dbo.Tasks')
)
BEGIN
    CREATE INDEX IX_Tasks_UserId ON dbo.Tasks (UserId);
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Tasks_Status'
      AND object_id = OBJECT_ID(N'dbo.Tasks')
)
BEGIN
    CREATE INDEX IX_Tasks_Status ON dbo.Tasks (Status);
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Tasks_Priority'
      AND object_id = OBJECT_ID(N'dbo.Tasks')
)
BEGIN
    CREATE INDEX IX_Tasks_Priority ON dbo.Tasks (Priority);
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Tasks_DueDate'
      AND object_id = OBJECT_ID(N'dbo.Tasks')
)
BEGIN
    CREATE INDEX IX_Tasks_DueDate ON dbo.Tasks (DueDate);
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'UX_Tasks_UserId_Title'
      AND object_id = OBJECT_ID(N'dbo.Tasks')
)
BEGIN
    CREATE UNIQUE INDEX UX_Tasks_UserId_Title
        ON dbo.Tasks (UserId, Title);
END;
GO
