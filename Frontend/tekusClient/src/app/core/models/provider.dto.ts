import { CustomFieldDto } from './customField.dto';
import { ServiceDto }      from './service.dto';

export interface ProviderDto {
  id: string;
  nit: string;
  name: string;
  email: string;
  customFields: CustomFieldDto[];
  services: ServiceDto[];
}

export interface CreateProviderDto {
  nit: string;
  name: string;
  email: string;
  customFields: CustomFieldDto[];
  services: ServiceDto[]; 
}

export interface AddCustomFieldDto {
  providerId: string; 
  key: string;
  value: string;
}

export interface UpdateProviderDto
  extends Pick<ProviderDto, 'id' | 'name' | 'nit' | 'email'> 
{
  services:     ServiceDto[];
  customFields: CustomFieldDto[];
}