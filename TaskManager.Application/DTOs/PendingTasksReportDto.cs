namespace TaskManager.Application.DTOs;

public class PendingTasksReportDto
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public int TotalPending { get; set; }

    public int TotalOverdue { get; set; }
}