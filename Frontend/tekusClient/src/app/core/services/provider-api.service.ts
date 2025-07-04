import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { ProviderDto, CreateProviderDto, UpdateProviderDto, AddCustomFieldDto } from '../models/provider.dto';
import { Observable } from 'rxjs/internal/Observable';


@Injectable({ providedIn: 'root' })
export class ProviderApiService {
  private api = inject(ApiService);

  list(params: any) {
    return this.api.get<ProviderDto[]>('providers', params);
  }
  getById(id: string) {
    return this.api.get<ProviderDto>(`providers/${id}`);
  }
  create(dto: CreateProviderDto) {
    return this.api.post<string>('providers', dto);
  }
update(id: string, dto: UpdateProviderDto): Observable<void> {
  return this.api.put<void>(`providers/${dto.id}`, dto);
}
  delete(id: string) {
    return this.api.delete<void>(`providers/${id}`);
  }
addCustomField(dto: AddCustomFieldDto) {
  return this.api.post<void>(`providers/${dto.providerId}/custom-fields`, dto);
}

}