import { Injectable, signal, computed } from '@angular/core';
import { CartItem } from '../models/pancake.model';

@Injectable({ providedIn: 'root' })
export class CartService {
  /* ── State ── */
  private _items = signal<CartItem[]>([]);
  private _toastMsg = signal<string>('');
  private _toastVisible = signal<boolean>(false);

  /* ── Computed ── */
  readonly items     = this._items.asReadonly();
  readonly toastMsg  = this._toastMsg.asReadonly();
  readonly toastVisible = this._toastVisible.asReadonly();

  readonly totalItems = computed(() =>
    this._items().reduce((sum, i) => sum + i.quantity, 0)
  );

  readonly totalPrice = computed(() =>
    this._items().reduce((sum, i) => sum + i.price * i.quantity, 0)
  );

  /* ── Actions ── */
  addItem(name: string, price: number): void {
    this._items.update((items) => {
      const existing = items.find((i) => i.name === name);
      if (existing) {
        return items.map((i) =>
          i.name === name ? { ...i, quantity: i.quantity + 1 } : i
        );
      }
      return [...items, { name, price, quantity: 1 }];
    });

    this.showToast(`${name} — $${price.toFixed(2)} added!`);
  }

  removeItem(name: string): void {
    this._items.update((items) => items.filter((i) => i.name !== name));
  }

  clearCart(): void {
    this._items.set([]);
  }

  showToast(message: string): void {
    this._toastMsg.set(message);
    this._toastVisible.set(true);

    setTimeout(() => this._toastVisible.set(false), 3000);
  }
}
