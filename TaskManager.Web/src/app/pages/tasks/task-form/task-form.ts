import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { TaskService } from '../../../core/services/task.service';
import { UserService } from '../../../core/services/user.service';

import { User } from '../../../models/user.model';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './task-form.html',
  styleUrl: './task-form.css'
})
export class TaskForm implements OnInit {

  private readonly fb = inject(FormBuilder);
  private readonly taskService = inject(TaskService);
  private readonly userService: UserService = inject(UserService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  users: User[] = [];

  loading = false;
  saving = false;

  isEditMode = false;
  taskId: number | null = null;

  taskForm: FormGroup = this.fb.group({
    title: [
      '',
      [
        Validators.required,
        Validators.maxLength(200)
      ]
    ],

    description: [
      '',
      [
        Validators.maxLength(500)
      ]
    ],

    priority: [
      1,
      Validators.required
    ],

    status: [
      0,
      Validators.required
    ],

    startDate: [
      null
    ],

    dueDate: [
      null,
      Validators.required
    ],

    userId: [
      null,
      Validators.required
    ]
  });

  ngOnInit(): void {

  this.loadUsers();

  const id = this.route.snapshot.paramMap.get('id');

  if (id) {

    this.isEditMode = true;
    this.taskId = Number(id);

    this.loadTask(this.taskId);
  }
}

  loadUsers(): void {

    this.loading = true;

    this.userService.getAll().subscribe({
      next: users => {

        this.users = users;

        this.loading = false;
      },

      error: error => {

        console.error('Error loading users:', error);

        this.loading = false;
      }
    });
  }

  save(): void {

  if (this.taskForm.invalid) {

    this.taskForm.markAllAsTouched();

    return;
  }

  const formValue = this.taskForm.value;

  const task = {
    title: formValue.title,
    description: formValue.description,
    priority: formValue.priority,
    status: formValue.status,
    startDate: formValue.startDate,
    dueDate: formValue.dueDate,
    userId: formValue.userId
  };

  console.log(
    this.isEditMode
      ? 'Actualizando tarea:'
      : 'Creando tarea:',
    task
  );

  this.saving = true;

  if (this.isEditMode && this.taskId !== null) {

    this.taskService.update(
      this.taskId,
      task
    ).subscribe({

      next: response => {

        console.log('Tarea actualizada:', response);

        this.saving = false;

        this.router.navigate(['/tasks']);
      },

      error: error => {

        console.error('Error updating task:', error);

        this.saving = false;
      }
    });

  } else {

    this.taskService.create(task).subscribe({

      next: response => {

        console.log('Tarea creada:', response);

        this.saving = false;

        this.router.navigate(['/tasks']);
      },

      error: error => {

        console.error('Error creating task:', error);

        this.saving = false;
      }
    });
  }
}

  loadTask(id: number): void {

  this.loading = true;

  this.taskService.getById(id).subscribe({

    next: task => {

      console.log('Tarea cargada para editar:', task);

      this.taskForm.patchValue({
        title: task.title,
        description: task.description,
        priority: task.priority,
        status: task.status,
        startDate: task.startDate
          ? this.toDateTimeLocal(task.startDate)
          : null,
        dueDate: task.dueDate
          ? this.toDateTimeLocal(task.dueDate)
          : null,
        userId: task.userId
      });

      this.loading = false;
    },

    error: error => {

      console.error('Error loading task:', error);

      this.loading = false;
    }
  });
}

toDateTimeLocal(date: string): string {

  const value = new Date(date);

  const year = value.getFullYear();
  const month = String(value.getMonth() + 1).padStart(2, '0');
  const day = String(value.getDate()).padStart(2, '0');

  const hours = String(value.getHours()).padStart(2, '0');
  const minutes = String(value.getMinutes()).padStart(2, '0');

  return `${year}-${month}-${day}T${hours}:${minutes}`;
}

  cancel(): void {

    this.router.navigate(['/tasks']);
  }

  get title() {
    return this.taskForm.get('title');
  }

  get description() {
    return this.taskForm.get('description');
  }

  get dueDate() {
    return this.taskForm.get('dueDate');
  }

  get userId() {
    return this.taskForm.get('userId');
  }
}