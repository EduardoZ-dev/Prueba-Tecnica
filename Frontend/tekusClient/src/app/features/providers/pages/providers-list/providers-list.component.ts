import { CommonModule} from '@angular/common';
import { MatTableModule}    from '@angular/material/table';
import { MatCardModule} from '@angular/material/card';
import { MatIconModule} from '@angular/material/icon';
import { MatButtonModule} from '@angular/material/button';
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ProviderDto } from '../../../../core/models/provider.dto';
import { ProviderApiService } from '../../../../core/services/provider-api.service';

@Component({
  selector: 'app-providers-list',
  imports: [CommonModule, MatTableModule, MatIconModule,
     MatButtonModule, MatCardModule],
  templateUrl: './providers-list.component.html',
  styleUrl: './providers-list.component.css'
})
export class ProvidersListComponent {
  private api = inject(ProviderApiService);
  private router = inject(Router);

  displayed  = ['nit','name','email','actions'];
  dataSource: ProviderDto[] = [];

  ngOnInit() {
    this.api.list({ page:1, pageSize:50 })
            .subscribe(res => this.dataSource = res);
  }

  goDetail(id:string) { this.router.navigate(['providers', id]); }

create() {
  this.router.navigate(['/providers/create']);
  }
}
