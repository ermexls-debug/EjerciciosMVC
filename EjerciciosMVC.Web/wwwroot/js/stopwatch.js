(function () {
    const display = document.getElementById("display");
    const startButton = document.getElementById("startButton");
    const pauseButton = document.getElementById("pauseButton");
    const lapButton = document.getElementById("lapButton");
    const resetButton = document.getElementById("resetButton");
    const lapsBody = document.getElementById("lapsBody");
    const lapsEmpty = document.getElementById("lapsEmpty");

    let elapsedBefore = 0;
    let startedAt = 0;
    let running = false;
    let frameId = 0;
    let laps = [];

    function currentElapsed() {
        if (running) {
            return elapsedBefore + (performance.now() - startedAt);
        }

        return elapsedBefore;
    }

    function pad(number) {
        return String(number).padStart(2, "0");
    }

    function format(milliseconds) {
        const totalCentiseconds = Math.floor(milliseconds / 10);
        const centiseconds = totalCentiseconds % 100;
        const totalSeconds = Math.floor(totalCentiseconds / 100);
        const seconds = totalSeconds % 60;
        const minutes = Math.floor(totalSeconds / 60) % 60;
        const hours = Math.floor(totalSeconds / 3600);

        let text = pad(minutes) + ":" + pad(seconds) + "." + pad(centiseconds);

        if (hours > 0) {
            text = pad(hours) + ":" + text;
        }

        return text;
    }

    function render() {
        display.textContent = format(currentElapsed());

        if (running) {
            frameId = requestAnimationFrame(render);
        }
    }

    function updateButtons() {
        startButton.disabled = running;
        pauseButton.disabled = !running;
        lapButton.disabled = !running;
        resetButton.disabled = running || currentElapsed() === 0;
        startButton.textContent = elapsedBefore > 0 ? "Reanudar" : "Iniciar";
    }

    function start() {
        if (running) {
            return;
        }

        running = true;
        startedAt = performance.now();
        updateButtons();
        render();
    }

    function pause() {
        if (!running) {
            return;
        }

        elapsedBefore = currentElapsed();
        running = false;
        cancelAnimationFrame(frameId);
        render();
        updateButtons();
    }

    function reset() {
        if (running) {
            return;
        }

        elapsedBefore = 0;
        laps = [];
        renderLaps();
        render();
        updateButtons();
    }

    function lap() {
        if (!running) {
            return;
        }

        const total = currentElapsed();
        let previousTotal = 0;

        if (laps.length > 0) {
            previousTotal = laps[laps.length - 1].total;
        }

        laps.push({
            number: laps.length + 1,
            lapTime: total - previousTotal,
            total: total
        });

        renderLaps();
    }

    function renderLaps() {
        lapsBody.innerHTML = "";
        lapsEmpty.style.display = laps.length === 0 ? "block" : "none";

        let fastest = null;
        let slowest = null;

        if (laps.length >= 2) {
            fastest = laps.reduce((a, b) => (b.lapTime < a.lapTime ? b : a));
            slowest = laps.reduce((a, b) => (b.lapTime > a.lapTime ? b : a));
        }

        for (let i = laps.length - 1; i >= 0; i--) {
            const item = laps[i];
            const row = document.createElement("tr");

            if (item === fastest) {
                row.className = "table-success";
            } else if (item === slowest) {
                row.className = "table-danger";
            }

            row.innerHTML = "<td>" + item.number + "</td><td>" + format(item.lapTime) + "</td><td>" + format(item.total) + "</td>";
            lapsBody.appendChild(row);
        }
    }

    startButton.addEventListener("click", start);
    pauseButton.addEventListener("click", pause);
    lapButton.addEventListener("click", lap);
    resetButton.addEventListener("click", reset);

    document.addEventListener("keydown", function (event) {
        if (event.target.tagName === "INPUT" || event.target.tagName === "TEXTAREA") {
            return;
        }

        if (event.code === "Space") {
            event.preventDefault();

            if (running) {
                pause();
            } else {
                start();
            }
        } else if (event.key === "v" || event.key === "V") {
            lap();
        }
    });
})();
