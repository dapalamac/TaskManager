IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        Id int IDENTITY(1, 1) NOT NULL,
        Name nvarchar(200) NOT NULL,
        Email nvarchar(320) NOT NULL,
        CONSTRAINT PK_Users PRIMARY KEY (Id)
    );
END;
GO

IF OBJECT_ID(N'dbo.Tasks', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Tasks
    (
        Id int IDENTITY(1, 1) NOT NULL,
        Title nvarchar(200) NOT NULL,
        Description nvarchar(1000) NULL,
        Priority int NOT NULL,
        CreatedAt datetime2 NOT NULL,
        StartDate datetime2 NULL,
        CompletedAt datetime2 NULL,
        DueDate datetime2 NULL,
        Status int NOT NULL,
        UserId int NOT NULL,
        CONSTRAINT PK_Tasks PRIMARY KEY (Id),
        CONSTRAINT FK_Tasks_Users_UserId
            FOREIGN KEY (UserId) REFERENCES dbo.Users (Id)
    );
END;
GO

IF OBJECT_ID(N'dbo.TaskStatusHistories', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TaskStatusHistories
    (
        Id int IDENTITY(1, 1) NOT NULL,
        TaskId int NOT NULL,
        OldStatus int NOT NULL,
        NewStatus int NOT NULL,
        ChangedAt datetime2 NOT NULL,
        ChangedByUserId int NOT NULL,
        CONSTRAINT PK_TaskStatusHistories PRIMARY KEY (Id),
        CONSTRAINT FK_TaskStatusHistories_Tasks_TaskId
            FOREIGN KEY (TaskId) REFERENCES dbo.Tasks (Id),
        CONSTRAINT FK_TaskStatusHistories_Users_ChangedByUserId
            FOREIGN KEY (ChangedByUserId) REFERENCES dbo.Users (Id)
    );
END;
GO

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
