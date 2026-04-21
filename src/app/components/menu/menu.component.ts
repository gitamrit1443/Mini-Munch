import { Component, inject } from '@angular/core';
import { RevealDirective } from '../../shared/services/reveal.directive';
import { CartService } from '../../shared/services/cart.service';
import { PancakeItem } from '../../shared/models/pancake.model';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [RevealDirective],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
})
export class MenuComponent {
  private cartService = inject(CartService);

  readonly pancakes: PancakeItem[] = [
    {
      id: 1,
      name: 'Classic Buttermilk Stack',
      description:
        'Fluffy triple-layered buttermilk pancakes kissed with Vermont maple syrup and a pat of cultured butter.',
      price: 14,
      rating: 4.9,
      reviewCount: 210,
      imageUrl:
        'https://images.unsplash.com/photo-1554520735-0a6b8b6ce8b7?w=600&q=80&auto=format&fit=crop',
      badge: 'Bestseller',
    },
    {
      id: 2,
      name: 'Wild Berry Cascade',
      description:
        'Golden pancakes crowned with macerated wild strawberries, blueberries, and lightly whipped mascarpone.',
      price: 17,
      rating: 4.8,
      reviewCount: 175,
      imageUrl:
        'https://images.unsplash.com/photo-1484723091739-30a097e8f929?w=600&q=80&auto=format&fit=crop',
      badge: "Chef's Pick",
    },
    {
      id: 3,
      name: 'Dark Cocoa Indulgence',
      description:
        'Belgian dark chocolate batter layered with hazelnut praline spread and shaved fine cocoa curls.',
      price: 18,
      rating: 4.9,
      reviewCount: 142,
      imageUrl:
        'https://images.unsplash.com/photo-1528207776546-365bb710ee93?w=600&q=80&auto=format&fit=crop',
    },
    {
      id: 4,
      name: 'Lemon Ricotta Bliss',
      description:
        'Airy ricotta-infused pancakes with a bright Meyer lemon glaze and a light dusting of powdered sugar.',
      price: 16,
      rating: 4.7,
      reviewCount: 89,
      imageUrl:
        'https://images.unsplash.com/photo-1590301157890-4810ed352733?w=600&q=80&auto=format&fit=crop',
      badge: 'New',
    },
    {
      id: 5,
      name: 'Caramelised Banana Royale',
      description:
        'Pan-seared banana slices in brown butter caramel over vanilla bean pancakes with a whisper of sea salt.',
      price: 17,
      rating: 4.8,
      reviewCount: 198,
      imageUrl:
        'https://images.unsplash.com/photo-1565299543923-37dd37887442?w=600&q=80&auto=format&fit=crop',
    },
    {
      id: 6,
      name: 'Nordic Honey Oat',
      description:
        'Hearty rolled-oat pancakes with wildflower honey drizzle, toasted pecans, and a whisper of cinnamon.',
      price: 15,
      rating: 4.9,
      reviewCount: 56,
      imageUrl:
        'https://images.unsplash.com/photo-1582169296194-e4d644c48063?w=600&q=80&auto=format&fit=crop',
      badge: 'Limited',
    },
  ];

  getStars(rating: number): string {
    const full  = Math.floor(rating);
    const empty = 5 - full;
    return '★'.repeat(full) + '☆'.repeat(empty);
  }

  addToCart(item: PancakeItem): void {
    this.cartService.addItem(item.name, item.price);
  }
}
