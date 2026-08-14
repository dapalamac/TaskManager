import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TaskService } from '../../core/services/task.service';
import { UserService } from '../../core/services/user.service';

import { Task } from '../../models/task.model';
import { User } from '../../models/user.model';
import { TaskFilter } from '../../models/task-filter.model';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css'
})
export class TasksComponent implements OnInit {

  private readonly taskService = inject(TaskService);
  private readonly userService = inject(UserService);

  tasks: Task[] = [];
  users: User[] = [];

  loading = false;

  page = 1;
  pageSize = 20;

  totalItems = 0;
  totalPages = 0;

  filter: TaskFilter = {
    page: this.page,
    pageSize: this.pageSize
  };

  ngOnInit(): void {
    this.loadUsers();
    this.loadTasks();
  }

  loadTasks(): void {
    this.loading = true;

    this.taskService.getAll({
      ...this.filter,
      page: this.page,
      pageSize: this.pageSize
    }).subscribe({
      next: response => {
        this.tasks = response.items;
        this.totalItems = response.totalItems;
        this.totalPages = response.totalPages;

        this.loading = false;
      },
      error: error => {
        console.error('Error loading tasks:', error);
        this.loading = false;
      }
    });
  }

  loadUsers(): void {
    this.userService.getAll().subscribe({
      next: users => {
        this.users = users;
      },
      error: error => {
        console.error('Error loading users:', error);
      }
    });
  }

  getUserName(userId: number): string {
    const user = this.users.find(u => u.id === userId);

    return user?.name ?? 'Sin asignar';
  }
}