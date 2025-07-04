export interface ServiceDto {
  id?: string;
  name: string;
  hourlyRateUsd: number;
  countries: string[]; 
}

export interface CreateServiceDto {
  providerId: string;
  name: string;
  hourlyRateUsd: number;
  countries: string[];
}

export interface UpdateServiceDto {
  id: string;
  name: string;
  hourlyRateUsd: number;
}

export interface DeleteServiceDto {
  id: string;
}