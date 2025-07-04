import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private base = environment.apiUrl;

  get<T>(url: string, params?: Record<string, any>) {
    const httpParams = new HttpParams({ fromObject: params as any });
    return this.http.get<T>(`${this.base}/${url}`, { params: httpParams });
  }
  post<T>(url: string, body: any) {
    return this.http.post<T>(`${this.base}/${url}`, body);
  }
  put<T>(url: string, body: any) {
    return this.http.put<T>(`${this.base}/${url}`, body);
  }
  delete<T>(url: string) {
    return this.http.delete<T>(`${this.base}/${url}`);
  }
}