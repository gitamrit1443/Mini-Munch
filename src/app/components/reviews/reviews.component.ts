import { Component } from '@angular/core';
import { RevealDirective } from '../../shared/services/reveal.directive';
import { Review } from '../../shared/models/pancake.model';

@Component({
  selector: 'app-reviews',
  standalone: true,
  imports: [RevealDirective],
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss'],
})
export class ReviewsComponent {
  readonly reviews: Review[] = [
    {
      id: 1,
      name: 'Sarah Kensington',
      role: 'Food Editor, London',
      avatarUrl:
        'https://images.unsplash.com/photo-1580489944761-15a19d654956?w=100&q=80&auto=format&fit=crop',
      rating: 5,
      text: `"I've had pancakes across three continents. Mini Munch is genuinely in a different league — the texture, the balance, the presentation. It's not breakfast, it's an experience."`,
    },
    {
      id: 2,
      name: 'James Redmond',
      role: 'Architect & Food Enthusiast',
      avatarUrl:
        'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=100&q=80&auto=format&fit=crop',
      rating: 5,
      text: '"The Wild Berry Cascade arrived warm, beautiful, and tasted exactly as refined as it sounds. I order every Sunday morning now — it\'s become a ritual I genuinely look forward to."',
      featured: true,
      verified: true,
    },
    {
      id: 3,
      name: 'Priya Mehta',
      role: 'Interior Designer, Dubai',
      avatarUrl:
        'https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=100&q=80&auto=format&fit=crop',
      rating: 5,
      text: '"The packaging alone impressed me before I even opened it. Inside — flawless. The Dark Cocoa Indulgence is a masterclass in restraint and richness at the same time."',
    },
  ];

  getStars(rating: number): string {
    return '★'.repeat(rating) + '☆'.repeat(5 - rating);
  }
}
