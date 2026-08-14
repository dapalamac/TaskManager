import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { User } from '../../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'https://localhost:7172/api/Users';

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  getById(id: number): Observable<User> {
    return this.http.get<User>(
      `${this.apiUrl}/${id}`
    );
  }

  create(user: Omit<User, 'id'>): Observable<User> {
    return this.http.post<User>(
      this.apiUrl,
      user
    );
  }

  update(id: number, user: User): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/${id}`,
      user
    );
  }
}