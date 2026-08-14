CREATE OR ALTER PROCEDURE dbo.sp_GetPendingTasks
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.Id AS UserId,
        u.Name AS UserName,
        SUM(CASE WHEN t.Status = 0 THEN 1 ELSE 0 END) AS TotalPending,
        SUM(
            CASE
                WHEN t.DueDate < GETDATE()
                 AND t.Status <> 2
                THEN 1
                ELSE 0
            END) AS TotalOverdue
    FROM dbo.Users AS u
    LEFT JOIN dbo.Tasks AS t ON t.UserId = u.Id
    GROUP BY
        u.Id,
        u.Name;
END;
GO
