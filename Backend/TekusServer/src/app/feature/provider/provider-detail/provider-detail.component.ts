import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ProviderService } from '../../../services/provider.service';
import { ProviderDto } from '../../../models/provider.dto';

@Component({
  selector: 'app-provider-detail',
  templateUrl: './provider-detail.component.html',
  styleUrls: ['./provider-detail.component.css']
})
export class ProviderDetailComponent implements OnInit {
  prov?: ProviderDto;
  form!: FormGroup;
  newCustomFieldForm = this.fb.group({
    key: ['', Validators.required],
    value: ['', Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private api: ProviderService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.api.getById(id).subscribe(p => {
      this.prov = p;
      this.form = this.fb.group({
        name: [p.name, Validators.required],
        nit: [p.nit, Validators.required],
        email: [p.email, [Validators.required, Validators.email]],
        customFields: this.fb.array(
          p.customFields.map(cf => this.fb.group({
            key: [cf.key, Validators.required],
            value: [cf.value, Validators.required]
          }))
        ),
        services: this.fb.array(
          p.services.map(s => this.fb.group({
            name: [s.name, Validators.required],
            hourlyRateUsd: [s.hourlyRateUsd, [Validators.required, Validators.min(1)]],
            countries: [s.countries.join(', '), Validators.required]
          }))
        )
      });
    });
  }

  get customFields() { return this.form.get('customFields') as FormArray; }
  get services() { return this.form.get('services') as FormArray; }

  addCustomFieldToProvider() {
    if (this.newCustomFieldForm.invalid || !this.prov) return;

    const dto = {
      providerId: this.prov.id,
      key: this.newCustomFieldForm.value.key!,
      value: this.newCustomFieldForm.value.value!
    };

    this.api.addCustomField(dto).subscribe({
      next: () => {
        this.api.getById(this.prov!.id).subscribe(p => {
          this.prov = p;
          this.form.setControl(
            'customFields',
            this.fb.array(
              p.customFields.map(cf => this.fb.group({
                key: [cf.key, Validators.required],
                value: [cf.value, Validators.required]
              }))
            )
          );
          this.newCustomFieldForm.reset();
        });
      },
      error: err => {
        alert('Error al agregar custom field');
      }
    });
  }
} 