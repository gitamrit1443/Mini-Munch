import { Component } from '@angular/core';
import { RevealDirective } from '../../shared/services/reveal.directive';

@Component({
  selector: 'app-cta',
  standalone: true,
  imports: [RevealDirective],
  templateUrl: './cta.component.html',
  styleUrls: ['./cta.component.scss'],
})
export class CtaComponent {
  readonly trustItems = [
    { icon: 'clock',   label: '30-min Delivery' },
    { icon: 'shield',  label: 'Freshness Guaranteed' },
    { icon: 'star',    label: '4.9★ Rated' },
    { icon: 'edit',    label: 'Fully Customisable' },
  ];

  scrollTo(href: string): void {
    document.querySelector(href)?.scrollIntoView({ behavior: 'smooth' });
  }
}
