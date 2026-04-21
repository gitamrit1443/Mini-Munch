import { Component, signal, computed, inject } from '@angular/core';
import { RevealDirective } from '../../shared/services/reveal.directive';
import { CartService } from '../../shared/services/cart.service';
import {
  SizeOption,
  Topping,
  SyrupOption,
  AddOn,
} from '../../shared/models/pancake.model';

@Component({
  selector: 'app-configurator',
  standalone: true,
  imports: [RevealDirective],
  templateUrl: './configurator.component.html',
  styleUrls: ['./configurator.component.scss'],
})
export class ConfiguratorComponent {
  private cartService = inject(CartService);

  /* ── Static data ── */
  readonly sizes: SizeOption[] = [
    { id: 'petit',   label: 'Petit',   count: 2, price: 10 },
    { id: 'classic', label: 'Classic', count: 3, price: 14 },
    { id: 'grand',   label: 'Grand',   count: 5, price: 19 },
  ];

  readonly allToppings: Topping[] = [
    { id: 'berries',  label: 'Fresh Berries' },
    { id: 'banana',   label: 'Banana Slices' },
    { id: 'pecans',   label: 'Candied Pecans' },
    { id: 'cream',    label: 'Whipped Cream' },
    { id: 'mascarpo', label: 'Mascarpone' },
    { id: 'cocoa',    label: 'Cocoa Flakes' },
    { id: 'caramel',  label: 'Caramel Crisp' },
    { id: 'coconut',  label: 'Coconut Flakes' },
  ];

  readonly syrups: SyrupOption[] = [
    { id: 'maple',     label: 'Vermont Maple' },
    { id: 'chocolate', label: 'Dark Chocolate' },
    { id: 'honey',     label: 'Wildflower Honey' },
    { id: 'salted',    label: 'Salted Caramel' },
    { id: 'berry',     label: 'Berry Coulis' },
    { id: 'lemon',     label: 'Lemon Glaze' },
  ];

  readonly addons: AddOn[] = [
    { id: 'butter', label: 'Extra Butter',    price: 1 },
    { id: 'bacon',  label: 'Side of Bacon',   price: 4 },
    { id: 'oj',     label: 'Fresh OJ',        price: 3 },
    { id: 'coffee', label: 'Artisan Coffee',  price: 4 },
    { id: 'yogurt', label: 'Yoghurt Cup',     price: 3 },
    { id: 'fruit',  label: 'Side Fruit Bowl', price: 5 },
  ];

  /* ── State (signals) ── */
  selectedSize      = signal<SizeOption>(this.sizes[1]);
  selectedToppings  = signal<Set<string>>(new Set(['berries']));
  selectedSyrup     = signal<string>('maple');
  selectedAddons    = signal<Set<string>>(new Set());

  /* ── Computed ── */
  readonly addonTotal = computed(() => {
    let total = 0;
    this.selectedAddons().forEach((id) => {
      const addon = this.addons.find((a) => a.id === id);
      if (addon) total += addon.price;
    });
    return total;
  });

  readonly orderTotal = computed(
    () => this.selectedSize().price + this.addonTotal()
  );

  readonly activeToppingsLabel = computed(() => {
    const labels = [...this.selectedToppings()]
      .map((id) => this.allToppings.find((t) => t.id === id)?.label ?? '')
      .filter(Boolean);
    return labels.length ? labels.join(', ') : 'None';
  });

  readonly activeSyrupLabel = computed(
    () => this.syrups.find((s) => s.id === this.selectedSyrup())?.label ?? ''
  );

  readonly activeAddonsLabel = computed(() => {
    const labels = [...this.selectedAddons()]
      .map((id) => this.addons.find((a) => a.id === id)?.label ?? '')
      .filter(Boolean);
    return labels.length ? labels.join(', ') : 'None';
  });

  /* ── Actions ── */
  selectSize(size: SizeOption): void {
    this.selectedSize.set(size);
  }

  toggleTopping(id: string): void {
    this.selectedToppings.update((set) => {
      const next = new Set(set);
      next.has(id) ? next.delete(id) : next.add(id);
      return next;
    });
  }

  selectSyrup(id: string): void {
    this.selectedSyrup.set(id);
  }

  toggleAddon(id: string): void {
    this.selectedAddons.update((set) => {
      const next = new Set(set);
      next.has(id) ? next.delete(id) : next.add(id);
      return next;
    });
  }

  isToppingActive(id: string): boolean {
    return this.selectedToppings().has(id);
  }

  isAddonActive(id: string): boolean {
    return this.selectedAddons().has(id);
  }

  addToCart(): void {
    const size  = this.selectedSize();
    const total = this.orderTotal();
    this.cartService.addItem(
      `Custom ${size.label} Stack`,
      total
    );
  }
}
