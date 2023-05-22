function handleButtonClick(animal) {
    if (animal === 'cat') {
        var result = 'cat';
        // Perform actions specific to 'cat'
        var activeLevelSelect = document.getElementById('activeLevelSelect');
        activeLevelSelect.innerHTML = ` 
                <option value="2.5">4개월 미만</option>
                <option value="2.5">4~6개월</option>
                <option value="2.0">7~12개월</option>
                <option value="1.2">중성화를 했으며 보통 활동량</option>
                <option value="1.4">중성화를 하지 않았으며 보통 활동량</option>
                <option value="1.0">비만 경향</option>
                <option value="0.8">체중 감량</option>
                <option value="1.6">많은 활동량/임신중</option>
            `;
    } else if (animal === 'dog') {
        var result = 'dog';
        // Perform actions specific to 'dog'
        var activeLevelSelect = document.getElementById('activeLevelSelect');
        activeLevelSelect.innerHTML = `
                <option value="3.0">4개월 미만</option>
                <option value="2.0">4~6개월</option>
                <option value="2.0">7~12개월</option>
                <option value="1.6">중성화를 했으며 보통 활동량</option>
                <option value="1.8">중성화를 하지 않았으며 보통 활동량</option>
                <option value="1.4">비만 경향</option>
                <option value="0.8">체중 감량</option>
                <option value="1.6">많은 활동량/임신중</option>
            `;
    }
}