import { Component, inject } from '@angular/core';
import { CartService } from '../../shared/services/cart.service';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [],
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.scss'],
})
export class ToastComponent {
  readonly cartService = inject(CartService);
}
