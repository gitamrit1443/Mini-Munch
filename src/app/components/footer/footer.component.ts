import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent {
  readonly year = new Date().getFullYear();

  readonly navLinks = [
    { label: 'Home',           href: '#hero' },
    { label: 'Our Menu',       href: '#menu' },
    { label: 'Specials',       href: '#specials' },
    { label: 'Build Your Order', href: '#configurator' },
    { label: 'About Us',       href: '#about' },
    { label: 'Reviews',        href: '#reviews' },
  ];

  readonly menuLinks = [
    'Classic Stacks',
    'Berry Series',
    'Chocolate Collection',
    'Seasonal Specials',
    "Chef's Signature",
    'Combo Deals',
  ];

  readonly legalLinks = [
    { label: 'Privacy Policy',  href: '#' },
    { label: 'Terms of Service', href: '#' },
    { label: 'Cookies',         href: '#' },
  ];

  scrollTo(href: string, event: Event): void {
    event.preventDefault();
    document.querySelector(href)?.scrollIntoView({ behavior: 'smooth' });
  }
}
