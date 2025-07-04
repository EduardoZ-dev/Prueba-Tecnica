import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'providers', pathMatch: 'full' },

  // LISTA
  {
    path: 'providers',
    loadComponent: () =>
      import('./features/providers/pages/providers-list/providers-list.component')
        .then(m => m.ProvidersListComponent)
  },

  // ⚠️  RUTA ESPECÍFICA ANTES DE LA CON PARAM
  {
    path: 'providers/create',
    loadComponent: () =>
      import('./features/providers/pages/provider-create/provider-create.component')
        .then(m => m.ProviderCreateComponent)
  },

  // DETALLE (parámetro)
  {
    path: 'providers/:id',
    loadComponent: () =>
      import('./features/providers/pages/provider-detail/provider-detail.component')
        .then(m => m.ProviderDetailComponent)
  },

  { path: '**', redirectTo: 'providers' }
];