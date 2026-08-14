export interface CreateTask {
  title: string;
  description: string | null;
  priority: number;
  startDate: string | null;
  dueDate: string | null;
  userId: number;
}