import { Component } from '@angular/core';
import { RevealDirective } from '../../shared/services/reveal.directive';

@Component({
  selector: 'app-values',
  standalone: true,
  imports: [RevealDirective],
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.scss'],
})
export class ValuesComponent {
  readonly cards = [
    {
      title: 'Artisan-Grade Ingredients',
      description:
        'We source only the finest organic flours, farm-fresh eggs, and seasonal produce from trusted local suppliers — because exceptional flavour starts at the source.',
    },
    {
      title: 'Chef-Crafted Recipes',
      description:
        'Every pancake creation is developed by our culinary team with fine dining experience — then perfected through months of refinement before it reaches your plate.',
    },
    {
      title: '30-Minute Delivery Guarantee',
      description:
        'Fresh, warm, and perfectly plated — delivered in under 30 minutes in temperature-controlled premium packaging designed to preserve every detail.',
    },
    {
      title: 'Fully Customisable Orders',
      description:
        'Build your perfect plate — choose your size, toppings, syrups, and add-ons. Mini Munch adapts entirely to your taste, every single time.',
    },
  ];
}
