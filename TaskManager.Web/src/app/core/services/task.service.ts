import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Task } from '../../models/task.model';
import { CreateTask } from '../../models/create-task.model';
import { UpdateTask } from '../../models/update-task.model';
import { TaskFilter } from '../../models/task-filter.model';
import { PagedResult } from '../../models/paged-result.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'https://localhost:7172/api/Tasks';

  getAll(filter: TaskFilter = {}): Observable<PagedResult<Task>> {

    let params = new HttpParams();

    if (filter.priority !== undefined) {
      params = params.set('priority', filter.priority);
    }

    if (filter.status !== undefined) {
      params = params.set('status', filter.status);
    }

    if (filter.userId !== undefined) {
      params = params.set('userId', filter.userId);
    }

    if (filter.startDate) {
      params = params.set('startDate', filter.startDate);
    }

    if (filter.endDate) {
      params = params.set('endDate', filter.endDate);
    }

    if (filter.page !== undefined) {
      params = params.set('page', filter.page);
    }

    if (filter.pageSize !== undefined) {
      params = params.set('pageSize', filter.pageSize);
    }

    return this.http.get<PagedResult<Task>>(
      this.apiUrl,
      { params }
    );
  }

  getById(id: number): Observable<Task> {
    return this.http.get<Task>(
      `${this.apiUrl}/${id}`
    );
  }

  create(task: CreateTask): Observable<Task> {
    return this.http.post<Task>(
      this.apiUrl,
      task
    );
  }

  update(id: number, task: UpdateTask): Observable<Task> {
    return this.http.put<Task>(
      `${this.apiUrl}/${id}`,
      task
    );
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
    );
  }
}