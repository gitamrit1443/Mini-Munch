import { Component } from '@angular/core';

import { NavbarComponent }       from './components/navbar/navbar.component';
import { HeroComponent }         from './components/hero/hero.component';
import { MenuComponent }         from './components/menu/menu.component';
import { ValuesComponent }       from './components/values/values.component';
import { ConfiguratorComponent } from './components/configurator/configurator.component';
import { SpecialsComponent }     from './components/specials/specials.component';
import { ReviewsComponent }      from './components/reviews/reviews.component';
import { AboutComponent }        from './components/about/about.component';
import { CtaComponent }          from './components/cta/cta.component';
import { FooterComponent }       from './components/footer/footer.component';
import { ToastComponent }        from './components/navbar/toast.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    NavbarComponent,
    HeroComponent,
    MenuComponent,
    ValuesComponent,
    ConfiguratorComponent,
    SpecialsComponent,
    ReviewsComponent,
    AboutComponent,
    CtaComponent,
    FooterComponent,
    ToastComponent,
  ],
  template: `
    <app-navbar />
    <main>
      <app-hero />
      <app-menu />
      <app-values />
      <app-configurator />
      <app-specials />
      <app-reviews />
      <app-about />
      <app-cta />
    </main>
    <app-footer />
    <app-toast />
  `,
  styles: [`
    main { display: block; }
  `],
})
export class AppComponent {}
