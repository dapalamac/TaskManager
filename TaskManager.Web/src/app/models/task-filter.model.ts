export interface TaskFilter {
  priority?: number;
  status?: number;
  userId?: number;
  startDate?: string;
  endDate?: string;
  page?: number;
  pageSize?: number;
}