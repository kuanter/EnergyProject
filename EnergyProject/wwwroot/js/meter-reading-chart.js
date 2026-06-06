/**
 * meter-reading-chart.js
 *
 * Initialises the MeterReading bar chart using Chart.js.
 *
 * Expected globals injected by the Razor view (via a <script> block):
 *   window.MeterReadingChart.labels  - string[]         (X-axis tick labels, pre-bucketed)
 *   window.MeterReadingChart.values  - (number|null)[]  (Y-axis kWh values; null = empty bucket)
 *   window.MeterReadingChart.filter  - string           ('Day' | 'Month' | 'Year' | 'All')
 */
(function () {
    'use strict';

    /** Returns the X-axis title that matches the active filter. */
    function xAxisTitle(filter) {
        switch (filter) {
            case 'Day':   return 'Hour of day';
            case 'Month': return 'Day';
            case 'Year':  return 'Month';
            default:      return 'Date';
        }
    }

    function initChart() {
        var cfg = window.MeterReadingChart;
        if (!cfg) {
            console.error('MeterReadingChart: config not found on window.');
            return;
        }

        var canvas = document.getElementById('meterReadingChart');
        if (!canvas) return;

        var ctx = canvas.getContext('2d');

        /* Gradient fill — applied only to non-null bars */
        var gradient = ctx.createLinearGradient(0, 0, 0, 360);
        gradient.addColorStop(0, 'rgba(99, 179, 237, 0.85)');
        gradient.addColorStop(1, 'rgba(49, 130, 206, 0.20)');

        var chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: cfg.labels,
                datasets: [{
                    label: 'ValueKWh',
                    data: cfg.values,           // null values → empty bars
                    backgroundColor: gradient,
                    borderColor: 'rgba(99, 179, 237, 1)',
                    borderWidth: 1.5,
                    borderRadius: 4,
                    borderSkipped: false,
                    skipNull: true              // Chart.js 4: skip null data points visually
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                animation: {
                    duration: 700,
                    easing: 'easeOutQuart'
                },
                plugins: {
                    legend: {
                        display: false
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipCtx) {
                                var val = tooltipCtx.parsed.y;
                                if (val === null || val === undefined) return ' No data';
                                return ' ' + val.toFixed(2) + ' kWh';
                            }
                        },
                        backgroundColor: 'rgba(15, 23, 42, 0.92)',
                        titleColor: '#93c5fd',
                        bodyColor: '#e2e8f0',
                        borderColor: 'rgba(99, 179, 237, 0.4)',
                        borderWidth: 1,
                        padding: 10
                    }
                },
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: xAxisTitle(cfg.filter),
                            color: '#94a3b8',
                            font: { size: 12, weight: '600' }
                        },
                        ticks: {
                            color: '#94a3b8',
                            maxRotation: 45,
                            minRotation: 30,
                            font: { size: 11 },
                            /* For Day/Month views with many ticks, auto-skip keeps it readable */
                            autoSkip: true,
                            maxTicksLimit: cfg.filter === 'Day' ? 24 : cfg.filter === 'Month' ? 15 : 12
                        },
                        grid: {
                            color: 'rgba(148, 163, 184, 0.1)'
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: 'Value (kWh)',
                            color: '#94a3b8',
                            font: { size: 12, weight: '600' }
                        },
                        ticks: {
                            color: '#94a3b8',
                            callback: function (val) {
                                return val.toFixed(2) + ' kWh';
                            },
                            font: { size: 11 }
                        },
                        grid: {
                            color: 'rgba(148, 163, 184, 0.12)'
                        },
                        beginAtZero: false
                    }
                }
            }
        });

        /**
         * Appended by SignalR in real time.
         * For bucketed filters (Day/Month/Year) this is a no-op — the bucket
         * model doesn't map cleanly to a live append, so the user should
         * refresh the page to re-aggregate. For 'All' it appends the new point.
         */
        cfg.push = function (isoTime, value) {
            if (cfg.filter !== 'All') return;   // buckets don't support live append

            var date = new Date(isoTime);
            var pad  = function (n) { return String(n).padStart(2, '0'); };
            var label =
                pad(date.getDate())  + '.' +
                pad(date.getMonth() + 1) + '.' +
                date.getFullYear()   + ' ' +
                pad(date.getHours()) + ':' +
                pad(date.getMinutes());

            chart.data.labels.push(label);
            chart.data.datasets[0].data.push(value);
            chart.update('active');
        };
    }

    /* Run after DOM + Chart.js are both ready */
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initChart);
    } else {
        initChart();
    }
})();
