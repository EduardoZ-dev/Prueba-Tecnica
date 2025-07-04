import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder, ReactiveFormsModule, Validators,
  FormArray, FormGroup, AbstractControl
} from '@angular/forms';
import { ProviderApiService } from '../../../../core/services/provider-api.service';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CreateProviderDto } from '../../../../core/models/provider.dto';

@Component({
  selector : 'app-provider-create',
  standalone: true,
  imports  : [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './provider-create.component.html',
  styleUrl   : './provider-create.component.css'
})
export class ProviderCreateComponent {

  private api    = inject(ProviderApiService);
  private router = inject(Router);
  private fb     = inject(FormBuilder);

  form = this.fb.group({
    nit  : ['', Validators.required],
    name : ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],

    customFields: this.fb.array<FormGroup>([]),

    services: this.fb.array<FormGroup>(
      [ this.newServiceForm() ],
      Validators.minLength(1)
    )
  });

  get customFields(): FormArray<FormGroup> {
    return this.form.get('customFields') as FormArray<FormGroup>;
  }

  get services(): FormArray<FormGroup> {
    return this.form.get('services') as FormArray<FormGroup>;
  }

  newCustomFieldForm(): FormGroup {
    return this.fb.group({
      key  : ['', Validators.required],
      value: ['', Validators.required]
    });
  }

  newServiceForm(): FormGroup {
    return this.fb.group({
      name         : ['', Validators.required],
      hourlyRateUsd: [1, [Validators.required, Validators.min(1)]],
      countries    : ['', Validators.required]   // texto separado por comas
    });
  }

  addCustomField(): void {
    this.customFields.push(this.newCustomFieldForm());
  }

  removeCustomField(idx: number): void {
    this.customFields.removeAt(idx);
  }

  addService(): void {
    this.services.push(this.newServiceForm());
  }

  removeService(idx: number): void {
    if (this.services.length > 1) {
      this.services.removeAt(idx);
    }
  }

  save(): void {
    if (this.form.invalid || this.services.length === 0) {
      alert('Debes agregar al menos un servicio');
      return;
    }

    type ServiceFormValue = {
      name: string;
      hourlyRateUsd: number;
      countries: string;
    };

    const dto: CreateProviderDto = {
      nit  : this.form.value.nit!,
      name : this.form.value.name!,
      email: this.form.value.email!,

      customFields: this.customFields.value.map(cf => ({
        key  : cf.key,
        value: cf.value
      })),

      services: this.services.value.map((s: ServiceFormValue) => ({
        name         : s.name,
        hourlyRateUsd: Number(s.hourlyRateUsd),
        countries    : s.countries.split(',').map(c => c.trim()).filter(c => !!c)
      }))
    };

    this.api.create(dto).subscribe({
      next : ()  => this.router.navigate(['/providers']),
      error: err => alert('Error al crear proveedor: ' + (err.error?.error || err.message))
    });
  }
}