import { Component, OnInit, ChangeDetectorRef, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TaskService } from '../../core/services/task.service';
import { UserService } from '../../core/services/user.service';

import { Task } from '../../models/task.model';
import { User } from '../../models/user.model';
import { TaskFilter } from '../../models/task-filter.model';

import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
  CommonModule,
  FormsModule,
  RouterLink
],
  templateUrl: './tasks.html',
  styleUrl: './tasks.css'
})
export class Tasks implements OnInit {

  private readonly taskService = inject(TaskService);
  private readonly userService = inject(UserService);
  private readonly cdr = inject(ChangeDetectorRef);

  tasks: Task[] = [];
  users: User[] = [];

  loading = false;

  page = 1;
  pageSize = 20;

  totalItems = 0;
  totalPages = 0;

  filter: TaskFilter = {
    page: 1,
    pageSize: 20
  };

  ngOnInit(): void {
    console.log('Tasks component iniciado');

    this.loadUsers();
    this.loadTasks();
  }

  loadTasks(): void {

  console.log('1. INICIO loadTasks');

  this.loading = true;

  this.taskService.getAll({
    ...this.filter,
    page: this.page,
    pageSize: this.pageSize
  }).subscribe({

    next: (response) => {

      console.log('2. RESPUESTA RECIBIDA');
      console.log(response);

      this.tasks = response.items;

      this.totalItems = response.totalItems;
      this.totalPages = response.totalPages;

      console.log('3. TAREAS:', this.tasks);

      this.loading = false;

      // Fuerza la actualización de la vista
      this.cdr.detectChanges();

      console.log('4. Vista actualizada');
    },

    error: (error) => {

      console.error('ERROR:', error);

      this.loading = false;

      this.cdr.detectChanges();
    }
  });
}

deleteTask(id: number): void {

  const confirmed = window.confirm(
    '¿Está seguro de que desea eliminar esta tarea?'
  );

  if (!confirmed) {
    return;
  }

  this.loading = true;

  this.taskService.delete(id).subscribe({

    next: () => {

      console.log('Tarea eliminada:', id);

      // Quitar inmediatamente la tarea de la tabla
      this.tasks = this.tasks.filter(task => task.id !== id);

      // Actualizar el total
      this.totalItems--;

      // Recargar desde la API para mantener los datos sincronizados
      this.loadTasks();
    },

    error: error => {

      console.error('Error eliminando tarea:', error);

      this.loading = false;
    }

  });
}

  loadUsers(): void {

    this.userService.getAll()
      .subscribe({
        next: (users) => {

          console.log('Usuarios recibidos:', users);

          this.users = users;
        },

        error: (error) => {

          console.error('Error loading users:', error);
        }
      });
  }

  getUserName(userId: number): string {

    const user = this.users.find(
      user => user.id === userId
    );

    return user?.name ?? 'Sin asignar';
  }

  getPriorityName(priority: number): string {

  switch (priority) {

    case 0:
      return 'Baja';

    case 1:
      return 'Media';

    case 2:
      return 'Alta';

    default:
      return 'Desconocida';
  }
}

getStatusName(status: number): string {

  switch (status) {

    case 0:
      return 'Pendiente';

    case 1:
      return 'En progreso';

    case 2:
      return 'Terminada';

    default:
      return 'Desconocido';
  }
}

applyFilters(): void {

  this.page = 1;

  this.filter.page = 1;
  this.filter.pageSize = this.pageSize;

  this.loadTasks();
}

previousPage(): void {

  if (this.page > 1) {

    this.page--;

    this.filter.page = this.page;

    this.loadTasks();
  }
}

nextPage(): void {

  if (this.page < this.totalPages) {

    this.page++;

    this.filter.page = this.page;

    this.loadTasks();
  }
}

}