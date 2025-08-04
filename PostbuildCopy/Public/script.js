const scoresList = document.getElementById('scores');

function updateScores(scores) {
    scoresList.innerHTML = '';
    scores.forEach(s => {
        const row = document.createElement('tr');
        const nameCell = document.createElement('td');
        const scoreCell = document.createElement('td');
        nameCell.textContent = s.EntryName;
        scoreCell.textContent = s.Score;
        row.appendChild(nameCell);
        row.appendChild(scoreCell);
        scoresList.appendChild(row);
    });
}

function fetchScores() {
    fetch('api/get-leaderboard')
        .then(r => r.json())
        .then(updateScores);
}

const ws = new WebSocket("ws://" + location.host + "/ws");
ws.onmessage = e => updateScores(JSON.parse(e.data));

fetchScores();
