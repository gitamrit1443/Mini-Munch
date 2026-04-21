# Mini Munch — Premium Pancake Ordering Website
---
## Project Overview
Mini Munch is a complete single-page premium pancake ordering website built with Angular 21.

---

## Tech Stack

| Technology       | Version  | Purpose                              |
|------------------|----------|--------------------------------------|
| Angular          | ^21.0.0  | Framework — standalone components    |
| Tailwind CSS     | ^3.4.0   | Utility-first CSS                    |
| Angular Signals  | Built-in | Reactive state (no NgRx needed)      |
| TypeScript       | ~5.5.0   | Type safety                          |
| SCSS             | Built-in | Component-level styles               |

---

## Project Structure

```
src/
├── app/
│   ├── components/
│   │   ├── navbar/
│   │   │   ├── navbar.component.ts       # Sticky nav, mobile menu, scroll state
│   │   │   ├── navbar.component.html
│   │   │   ├── navbar.component.scss
│   │   │   ├── toast.component.ts        # Global toast notification
│   │   │   ├── toast.component.html
│   │   │   └── toast.component.scss
│   │   ├── hero/                         # Cinematic full-bleed hero
│   │   ├── menu/                         # 6-card pancake showcase grid
│   │   ├── values/                       # Brand value proposition (dark section)
│   │   ├── configurator/                 # Interactive pancake builder
│   │   ├── specials/                     # Limited-edition chef banner
│   │   ├── reviews/                      # Customer testimonials grid
│   │   ├── about/                        # Brand story + metrics
│   │   ├── cta/                          # Final conversion section
│   │   └── footer/                       # Premium minimal footer
│   ├── shared/
│   │   ├── models/
│   │   │   └── pancake.model.ts          # All TypeScript interfaces
│   │   └── services/
│   │       ├── cart.service.ts           # Signal-based cart + toast
│   │       └── reveal.directive.ts       # IntersectionObserver scroll reveal
│   ├── app.component.ts                  # Root shell component
│   ├── app.config.ts                     # provideRouter, provideAnimations
│   └── app.routes.ts                     # Route definitions
├── styles.scss                           # Global CSS variables + shared utilities
└── index.html
```

---

## Getting Started

### 1. Install dependencies


npm install


### 2. Run development server

ng serve or ng s-o(directly opens in default browser)


Open [http://localhost:4200](http://localhost:4200)
---
