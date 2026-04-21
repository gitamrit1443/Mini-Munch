import {
  Directive,
  ElementRef,
  Input,
  OnInit,
  OnDestroy,
  inject,
} from '@angular/core';

@Directive({
  selector: '[appReveal]',
  standalone: true,
})
export class RevealDirective implements OnInit, OnDestroy {
  @Input('appReveal') delay: number = 0; // delay in ms

  private el = inject(ElementRef);
  private observer!: IntersectionObserver;

  ngOnInit(): void {
    const nativeEl: HTMLElement = this.el.nativeElement;
    nativeEl.classList.add('reveal');
    if (this.delay) nativeEl.style.transitionDelay = `${this.delay}ms`;

    this.observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            nativeEl.classList.add('visible');
            this.observer.unobserve(nativeEl);
          }
        });
      },
      { threshold: 0.12, rootMargin: '0px 0px -40px 0px' }
    );

    this.observer.observe(nativeEl);
  }

  ngOnDestroy(): void {
    this.observer?.disconnect();
  }
}
