'use strict';

/* ── Live chart update via SignalR ── */
(function () {
    const RECONNECT_DELAYS = [2000, 5000, 10000];

    function formatDate(isoString) {
        const d = new Date(isoString);
        const p = n => String(n).padStart(2, '0');
        return `${p(d.getDate())}.${p(d.getMonth() + 1)}.${d.getFullYear()} ${p(d.getHours())}:${p(d.getMinutes())}:${p(d.getSeconds())}`;
    }

    function prependRow(tbody, data) {
        const row = tbody.insertRow(0);
        row.classList.add('row-live');
        row.insertCell(0).textContent = data.id;
        row.insertCell(1).textContent = formatDate(data.createdAt);
        const valCell = row.insertCell(2);
        valCell.innerHTML = `<span class="chip chip-dark">${data.valueKWh} kWh</span>`;
    }

    function updateLiveStats(data) {
        const el = document.getElementById('live-last-value');
        if (el) el.textContent = `${parseFloat(data.valueKWh).toFixed(2)} kWh`;

        const ts = document.getElementById('live-last-time');
        if (ts) ts.textContent = formatDate(data.createdAt);

        const dot = document.getElementById('live-dot');
        if (dot) {
            dot.classList.add('live-dot--pulse');
            setTimeout(() => dot.classList.remove('live-dot--pulse'), 1200);
        }
    }

    function initSignalR(meterId) {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl('/meterHub')
            .withAutomaticReconnect(RECONNECT_DELAYS)
            .configureLogging(signalR.LogLevel.Warning)
            .build();

        connection.on('ReceiveReading', data => {
            const tbody = document.getElementById('readings');
            if (tbody) prependRow(tbody, data);

            // Push to chart (no-op for non-All filters)
            const cfg = window.MeterReadingChart;
            if (cfg?.push) cfg.push(data.createdAt, data.valueKWh);

            updateLiveStats(data);
        });

        connection.onreconnecting(() => setStatus('reconnecting'));
        connection.onreconnected(() => setStatus('online'));
        connection.onclose(() => setStatus('offline'));

        connection.start()
            .then(() => {
                connection.invoke('Subscribe', meterId);
                setStatus('online');
            })
            .catch(err => {
                console.error('SignalR connect error:', err);
                setStatus('offline');
            });
    }

    function setStatus(state) {
        const badge = document.getElementById('live-status');
        if (!badge) return;
        badge.className = `live-status live-status--${state}`;
        badge.textContent = { online: 'Live', reconnecting: 'Reconnecting…', offline: 'Offline' }[state] ?? state;
    }

    function init() {
        const el = document.getElementById('meter-id-data');
        if (!el) return;
        const meterId = el.dataset.meterId;
        if (meterId) initSignalR(meterId);
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
