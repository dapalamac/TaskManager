export interface UpdateTask {
  title: string;
  description: string | null;
  priority: number;
  status: number;
  startDate: string | null;
  dueDate: string | null;
  userId: number;
}