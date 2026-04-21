import { Component, inject } from '@angular/core';
import { RevealDirective } from '../../shared/services/reveal.directive';
import { CartService } from '../../shared/services/cart.service';

@Component({
  selector: 'app-specials',
  standalone: true,
  imports: [RevealDirective],
  templateUrl: './specials.component.html',
  styleUrls: ['./specials.component.scss'],
})
export class SpecialsComponent {
  private cartService = inject(CartService);

  readonly tags = [
    'Black Truffle',
    'Morello Cherry',
    'Valrhona Ganache',
    'Soufflé Style',
  ];

  reserve(): void {
    this.cartService.showToast('Truffle & Black Cherry Stack reserved!');
    document.querySelector('#configurator')?.scrollIntoView({ behavior: 'smooth' });
  }
}
