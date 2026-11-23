(function () {
    const board = document.getElementById("board");
    const cards = Array.from(board.querySelectorAll(".memory-card"));
    const movesText = document.getElementById("moves");
    const timeText = document.getElementById("time");
    const pairsText = document.getElementById("pairs");
    const winPanel = document.getElementById("winPanel");

    const totalPairs = parseInt(board.dataset.pairs);
    const level = parseInt(board.dataset.level);

    let firstCard = null;
    let secondCard = null;
    let locked = false;
    let moves = 0;
    let matchedPairs = 0;
    let seconds = 0;
    let timerId = null;

    function startTimer() {
        if (timerId !== null) {
            return;
        }

        timerId = setInterval(function () {
            seconds++;
            timeText.textContent = seconds;
        }, 1000);
    }

    function stopTimer() {
        clearInterval(timerId);
    }

    function showCard(card) {
        card.textContent = card.dataset.symbol;
        card.classList.add("flipped");
    }

    function hideCard(card) {
        card.textContent = "?";
        card.classList.remove("flipped");
    }

    function calculatePoints() {
        const points = totalPairs * 100 - (moves - totalPairs) * 10 - seconds;
        return Math.max(0, points) * level;
    }

    function finishGame() {
        stopTimer();
        document.getElementById("winMoves").textContent = moves;
        document.getElementById("winTime").textContent = seconds;
        document.getElementById("winPoints").textContent = calculatePoints();
        winPanel.style.display = "block";
    }

    function checkPair() {
        moves++;
        movesText.textContent = moves;

        if (firstCard.dataset.symbol === secondCard.dataset.symbol) {
            firstCard.classList.add("matched");
            secondCard.classList.add("matched");
            firstCard.disabled = true;
            secondCard.disabled = true;
            matchedPairs++;
            pairsText.textContent = matchedPairs;
            firstCard = null;
            secondCard = null;

            if (matchedPairs === totalPairs) {
                finishGame();
            }

            return;
        }

        locked = true;

        setTimeout(function () {
            hideCard(firstCard);
            hideCard(secondCard);
            firstCard = null;
            secondCard = null;
            locked = false;
        }, 800);
    }

    function onCardClick(event) {
        const card = event.currentTarget;

        if (locked || card === firstCard || card.classList.contains("matched")) {
            return;
        }

        startTimer();
        showCard(card);

        if (firstCard === null) {
            firstCard = card;
            return;
        }

        secondCard = card;
        checkPair();
    }

    cards.forEach(function (card) {
        card.addEventListener("click", onCardClick);
    });
})();
