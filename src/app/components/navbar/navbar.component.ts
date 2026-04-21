import {
  Component,
  HostListener,
  signal,
  inject,
  computed,
} from '@angular/core';
import { CartService } from '../../shared/services/cart.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  private cartService = inject(CartService);

  isScrolled  = signal(false);
  mobileOpen  = signal(false);
  cartCount   = this.cartService.totalItems;

  readonly navLinks = [
    { label: 'Home',     href: '#hero' },
    { label: 'Menu',     href: '#menu' },
    { label: 'Specials', href: '#specials' },
    { label: 'About',    href: '#about' },
    { label: 'Reviews',  href: '#reviews' },
    { label: 'Order',    href: '#configurator' },
  ];

  @HostListener('window:scroll')
  onScroll(): void {
    this.isScrolled.set(window.scrollY > 20);
  }

  toggleMobile(): void {
    this.mobileOpen.update((v) => !v);
  }

  closeMobile(): void {
    this.mobileOpen.set(false);
  }

  scrollTo(href: string): void {
    this.closeMobile();
    const el = document.querySelector(href);
    el?.scrollIntoView({ behavior: 'smooth' });
  }
}
