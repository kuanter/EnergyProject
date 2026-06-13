'use strict';

(function () {
    const X_TITLES = { Day: 'Hour of day', Month: 'Day', Year: 'Month' };

    const pad = n => String(n).padStart(2, '0');

    /** Converts a Date to the bucket label string that matches the Razor-generated label for the given filter. */
    function toBucketLabel(date, filter) {
        switch (filter) {
            case 'Day':   return `${pad(date.getHours())}:00`;
            case 'Month': return `${pad(date.getDate())}.${pad(date.getMonth() + 1)}`;
            case 'Year':  return `${pad(date.getMonth() + 1)}.${date.getFullYear()}`;
            default:
                return `${pad(date.getDate())}.${pad(date.getMonth() + 1)}.${date.getFullYear()} ${pad(date.getHours())}:${pad(date.getMinutes())}`;
        }
    }

    function buildGradient(ctx) {
        const g = ctx.createLinearGradient(0, 0, 0, 360);
        g.addColorStop(0, 'rgba(99, 179, 237, 0.85)');
        g.addColorStop(1, 'rgba(49, 130, 206, 0.20)');
        return g;
    }

    function createChart(canvas, cfg) {
        const ctx = canvas.getContext('2d');

        return new Chart(ctx, {
            type: 'bar',
            data: {
                labels: cfg.labels,
                datasets: [{
                    label: 'kWh',
                    data: cfg.values,
                    backgroundColor: buildGradient(ctx),
                    borderColor: 'rgba(99, 179, 237, 1)',
                    borderWidth: 1.5,
                    borderRadius: 4,
                    borderSkipped: false,
                    skipNull: true
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                animation: { duration: 500, easing: 'easeOutQuart' },
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        backgroundColor: 'rgba(15, 23, 42, 0.92)',
                        titleColor: '#93c5fd',
                        bodyColor: '#e2e8f0',
                        borderColor: 'rgba(99, 179, 237, 0.4)',
                        borderWidth: 1,
                        padding: 10,
                        callbacks: {
                            label: item => item.parsed.y == null ? ' No data' : ` ${item.parsed.y.toFixed(2)} kWh`
                        }
                    }
                },
                scales: {
                    x: {
                        title: { display: true, text: X_TITLES[cfg.filter] ?? 'Date', color: '#94a3b8', font: { size: 12, weight: '600' } },
                        ticks: {
                            color: '#94a3b8',
                            maxRotation: 45,
                            minRotation: 30,
                            font: { size: 11 },
                            autoSkip: true,
                            maxTicksLimit: cfg.filter === 'Day' ? 24 : cfg.filter === 'Month' ? 15 : 12
                        },
                        grid: { color: 'rgba(148, 163, 184, 0.1)' }
                    },
                    y: {
                        title: { display: true, text: 'Value (kWh)', color: '#94a3b8', font: { size: 12, weight: '600' } },
                        ticks: {
                            color: '#94a3b8',
                            font: { size: 11 },
                            callback: val => `${val.toFixed(2)} kWh`
                        },
                        grid: { color: 'rgba(148, 163, 184, 0.12)' },
                        beginAtZero: false
                    }
                }
            }
        });
    }

    function initChart() {
        const cfg = window.MeterReadingChart;
        if (!cfg) { console.error('MeterReadingChart: config missing'); return; }

        const canvas = document.getElementById('meterReadingChart');
        if (!canvas) return;

        const chart = createChart(canvas, cfg);
        const dataset = chart.data.datasets[0];

        /**
         * Called by meter-live.js on every SignalR "ReceiveReading".
         * Works for ALL filter modes:
         *   Day/Month/Year — finds the matching bucket and replaces its value.
         *   All            — appends a new data point.
         */
        cfg.push = function (isoTime, value) {
            const date  = new Date(isoTime);
            const label = toBucketLabel(date, cfg.filter);

            if (cfg.filter === 'All') {
                chart.data.labels.push(label);
                dataset.data.push(value);
            } else {
                const idx = chart.data.labels.indexOf(label);
                if (idx !== -1) {
                    dataset.data[idx] = value;
                }
            }

            chart.update('active');
        };
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initChart);
    } else {
        initChart();
    }
})();
