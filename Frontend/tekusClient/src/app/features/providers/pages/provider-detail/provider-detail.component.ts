import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProviderDto, UpdateProviderDto } from '../../../../core/models/provider.dto';
import { ProviderApiService } from '../../../../core/services/provider-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-provider-detail',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    // MatFormField, MatCard, MatLabel, MatCardContent, MatCardTitle, MatCardSubtitle
  ],
  templateUrl: './provider-detail.component.html',
  styleUrls: ['./provider-detail.component.css']
})
export class ProviderDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private api   = inject(ProviderApiService);
  private fb    = inject(FormBuilder);
  private router = inject(Router);

  prov?: ProviderDto;
  form!: FormGroup;
  newCustomFieldForm!: FormGroup;

  ngOnInit() {
    // 1) Inicializamos el formulario para agregar un custom field
    this.newCustomFieldForm = this.fb.group({
      key:    ['', Validators.required],
      value: ['', Validators.required]
    });

    // 2) Cargamos el proveedor y armamos el formulario principal
    const id = this.route.snapshot.paramMap.get('id')!;
    this.api.getById(id).subscribe(p => {
      this.prov = p;
      this.form = this.fb.group({
        name:        [p.name, Validators.required],
        nit:         [p.nit, Validators.required],
        email:       [p.email, [Validators.required, Validators.email]],
        customFields: this.fb.array(
          p.customFields.map(cf => this.fb.group({
            key:   [cf.key, Validators.required],
            value: [cf.value, Validators.required]
          }))
        ),
        services: this.fb.array(
          p.services.map(s => this.fb.group({
            name:            [s.name, Validators.required],
            hourlyRateUsd:   [s.hourlyRateUsd, [Validators.required, Validators.min(1)]],
            countries:       [s.countries.join(', '), Validators.required]
          }))
        )
      });
    });
  }

  // Getters para los FormArray
  get customFields() { return this.form.get('customFields') as FormArray; }
  get services()     { return this.form.get('services')     as FormArray; }

  // Métodos para agregar/quitar dinámicamente customFields
  addCustomField() {
    this.customFields.push(this.fb.group({
      key:    ['', Validators.required],
      value: ['', Validators.required]
    }));
  }
  removeCustomField(i: number) {
    this.customFields.removeAt(i);
  }

  // Métodos para agregar/quitar servicios
  addService() {
    this.services.push(this.fb.group({
      name:          ['', Validators.required],
      hourlyRateUsd: [1, [Validators.required, Validators.min(1)]],
      countries:     ['', Validators.required]
    }));
  }
  removeService(i: number) {
    this.services.removeAt(i);
  }

  // Guardar datos generales del proveedor
save() {
  if (this.form.invalid) return;

  const value = this.form.value;

  const dto = {
    id: this.prov!.id,
    nit: value.nit,
    name: value.name,
    email: value.email,
    customFields: value.customFields ?? [],
    services: value.services?.map((s: any) => ({
      name: s.name,
      hourlyRateUsd: Number(s.hourlyRateUsd),
      countries: s.countries
        ? s.countries.split(',').map((c: string) => c.trim()).filter((c: string) => !!c)
        : []
    })) ?? []
  };

  this.api.update(this.prov!.id, dto).subscribe({
    next: () => {
      alert('Proveedor actualizado');
      this.router.navigate(['/']); // 👈 redirige al inicio
    },
    error: err => {
      alert('Ocurrió un error al actualizar el proveedor');
    }
  });
}

  // Agregar un custom field vía el endpoint dedicado
  addCustomFieldToProvider() {
    if (this.newCustomFieldForm.invalid || !this.prov) return;

    const dto = {
      providerId: this.prov.id,
      key:        this.newCustomFieldForm.value.key,
      value:      this.newCustomFieldForm.value.value
    };

    this.api.addCustomField(dto).subscribe({
      next: () => {
        // Refrescar el proveedor y el array de customFields
        this.api.getById(this.prov!.id).subscribe(p => {
          this.prov = p;
          this.form.setControl(
            'customFields',
            this.fb.array(
              p.customFields.map(cf => this.fb.group({
                key:   [cf.key, Validators.required],
                value: [cf.value, Validators.required]
              }))
            )
          );
          this.newCustomFieldForm.reset();
        });
      },
      error: () => {
        alert('Error al agregar custom field');
      }
    });
  }
}
