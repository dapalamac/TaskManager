import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { PendingTaskReport } from '../../models/pending-task-report.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'https://localhost:7172/api/Reports';

  getPendingTasks(): Observable<PendingTaskReport[]> {

    return this.http.get<PendingTaskReport[]>(
      `${this.apiUrl}/pending-tasks`
    );
  }
}