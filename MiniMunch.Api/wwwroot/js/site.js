document.addEventListener('DOMContentLoaded', () => {
  document.querySelectorAll('[data-auto-hide]').forEach((el) => {
    setTimeout(() => el.remove(), 3500);
  });
});
