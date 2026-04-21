import { Component } from '@angular/core';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [],
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss'],
})
export class HeroComponent {
  readonly trustItems = [
    { label: 'Handcrafted Daily', icon: 'sun' },
    { label: 'Fresh Ingredients', icon: 'shield' },
    { label: 'Fast Delivery',     icon: 'truck' },
  ];

  scrollTo(href: string): void {
    document.querySelector(href)?.scrollIntoView({ behavior: 'smooth' });
  }
}
