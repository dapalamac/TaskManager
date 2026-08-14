import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'tasks',
    pathMatch: 'full'
  },
  {
  path: 'tasks/new',
  loadComponent: () =>
    import('./pages/tasks/task-form/task-form')
      .then(m => m.TaskForm)
 },
 {
  path: 'tasks/edit/:id',
  loadComponent: () =>
    import('./pages/tasks/task-form/task-form')
      .then(m => m.TaskForm)
},
  {
    path: 'tasks',
    loadComponent: () =>
      import('./pages/tasks/tasks')
        .then(m => m.Tasks)
  },
  {
  path: 'reports',
  loadComponent: () =>
    import('./pages/reports/reports')
      .then(m => m.Reports)
},
  {
    path: 'reports',
    loadComponent: () =>
      import('./pages/reports/reports')
        .then(m => m.Reports)
  },
  {
    path: '**',
    redirectTo: 'tasks'
  }
];