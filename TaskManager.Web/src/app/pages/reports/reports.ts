import { Component, OnInit, ChangeDetectorRef, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportService } from '../../core/services/report.service';
import { PendingTaskReport } from '../../models/pending-task-report.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [
  CommonModule,
  RouterLink
],
  templateUrl: './reports.html',
  styleUrl: './reports.css'
})
export class Reports implements OnInit {

  private readonly reportService = inject(ReportService);
  private readonly cdr = inject(ChangeDetectorRef);

  reports: PendingTaskReport[] = [];

  loading = false;

  ngOnInit(): void {
    this.loadReport();
  }

  loadReport(): void {

    this.loading = true;

    this.reportService.getPendingTasks().subscribe({

      next: response => {

        console.log('Reporte recibido:', response);

        this.reports = response;

        this.loading = false;

        this.cdr.detectChanges();
      },

      error: error => {

        console.error(
          'Error cargando reporte:',
          error
        );

        this.loading = false;

        this.cdr.detectChanges();
      }
    });
  }
}