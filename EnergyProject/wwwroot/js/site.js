// Тема (light / dark) для EnergyProject
(function () {
    const STORAGE_KEY = 'energy-theme';

    function applyTheme(theme) {
        const t = theme === 'dark' ? 'dark' : 'light';

        document.body.classList.remove('theme-light', 'theme-dark');
        document.body.classList.add('theme-' + t);

        const btn = document.getElementById('themeToggle');
        if (btn) {
            btn.setAttribute('data-theme', t);
            btn.setAttribute('aria-pressed', t === 'dark');
        }

        try {
            localStorage.setItem(STORAGE_KEY, t);
        } catch (e) {
            // якщо localStorage не доступний – просто ігноруємо
        }
    }

    document.addEventListener('DOMContentLoaded', function () {
        let stored = null;
        try {
            stored = localStorage.getItem(STORAGE_KEY);
        } catch (e) { }

        const prefersDark = window.matchMedia &&
            window.matchMedia('(prefers-color-scheme: dark)').matches;

        applyTheme(stored || (prefersDark ? 'dark' : 'light'));

        const btn = document.getElementById('themeToggle');
        if (!btn) return;

        btn.addEventListener('click', function () {
            const isDark = document.body.classList.contains('theme-dark');
            applyTheme(isDark ? 'light' : 'dark');
        });
    });
})();
