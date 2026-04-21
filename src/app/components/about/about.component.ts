import { Component } from '@angular/core';
import { RevealDirective } from '../../shared/services/reveal.directive';
import { BrandMetric } from '../../shared/models/pancake.model';

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [RevealDirective],
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
})
export class AboutComponent {
  readonly metrics: BrandMetric[] = [
    { value: '50', suffix: 'k+', label: 'Happy Customers' },
    { value: '300', suffix: '+',  label: 'Daily Orders' },
    { value: '100', suffix: '%',  label: 'Fresh Ingredients' },
    { value: '18',  suffix: '+',  label: 'Signature Recipes' },
  ];
}
