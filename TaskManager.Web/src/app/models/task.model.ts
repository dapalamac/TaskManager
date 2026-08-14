export interface Task {
  id: number;
  title: string;
  description: string | null;
  priority: number;
  createdAt: string;
  startDate: string | null;
  completedAt: string | null;
  dueDate: string | null;
  status: number;
  userId: number;
}