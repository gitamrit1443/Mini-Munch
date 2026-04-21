/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      colors: {
        ivory:      '#FAF8F3',
        cream:      '#F4EFE4',
        beige:      '#EDE4D3',
        champagne:  '#E2D5BF',
        caramel:    '#C8A87A',
        gold:       '#B8891C',
        amber:      '#D4A040',
        espresso:   '#1E0F07',
        choco:      '#3B1F0E',
        mocha:      '#5C3520',
        charcoal:   '#1A1814',
        'warm-dark':'#1C1410',
      },
      fontFamily: {
        display: ["'Cormorant Garamond'", 'Georgia', 'serif'],
        body:    ["'Jost'", 'sans-serif'],
      },
      fontSize: {
        'display-xl': ['5.6rem', { lineHeight: '1.06', letterSpacing: '-0.025em' }],
        'display-lg': ['3.6rem', { lineHeight: '1.12', letterSpacing: '-0.01em' }],
      },
      borderRadius: {
        'card': '16px',
      },
      boxShadow: {
        'card-hover': '0 24px 56px rgba(30,15,7,0.11)',
        'premium':    '0 24px 48px rgba(30,15,7,0.12)',
        'nav':        '0 4px 32px rgba(30,15,7,0.08)',
      },
      animation: {
        'fade-up':  'fadeUp 0.9s cubic-bezier(0.25,0.46,0.45,0.94) both',
        'fade-in':  'fadeIn 1.1s 0.3s cubic-bezier(0.25,0.46,0.45,0.94) both',
      },
      keyframes: {
        fadeUp:  { from: { opacity:'0', transform:'translateY(28px)' }, to: { opacity:'1', transform:'translateY(0)' } },
        fadeIn:  { from: { opacity:'0' }, to: { opacity:'1' } },
      },
    },
  },
  plugins: [],
};
