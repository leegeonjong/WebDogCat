function handleButtonClick(animal) {
    if (animal === 'cat') {
        //이미지에 border등 주기
        document.querySelector('.cat').style.opacity = "1";
        document.querySelector('.cat').style.border = "5px solid rgb(0, 103, 163)";
        document.querySelector('.cat').style.borderRadius = "10px";
        document.querySelector('.dog').style.opacity = "0.7";
        document.querySelector('.dog').style.border = "0px";
 

        document.getElementById("dogForm").style.display = "none";
        document.getElementById("catForm").style.display = "block";
        document.getElementById("activeLevelSelectCat").innerHTML = `
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
        //이미지에 border등 주기
        document.querySelector('.dog').style.opacity = "1";
        document.querySelector('.dog').style.border = "5px solid rgb(0, 103, 163)";
        document.querySelector('.dog').style.borderRadius = "10px";
        document.querySelector('.cat').style.opacity = "0.7";
        document.querySelector('.cat').style.border = "0px";

        document.getElementById("catForm").style.display = "none";
        document.getElementById("dogForm").style.display = "block";
        document.getElementById("activeLevelSelectDog").innerHTML = `
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
function calculateCalorieCat() {
    var weight = parseFloat(document.getElementById("WeightCat").value);
    var activeLevelCat = parseFloat(document.getElementById("activeLevelSelectCat").value);
    var errorElement = document.getElementById("errorCat");

    if (!isNaN(weight) && !isNaN(activeLevelCat)) {
        var RER = 0.0;

        if (weight > 2.0) {
            RER = 70 * (weight * 0.75);
        } else {
            RER = 30 * weight + 70;
        }

        var calorie = RER * activeLevelCat;
        document.getElementById("CalorieCat").value = calorie.toFixed(2) + " kcal";
        displayErrorIdMessage(errorElement, "");
    }
    else if (weight === NaN) {
        displayErrorIdMessage(errorElement, "몸무게를 입력하세요.");
        return false;
    }
    else if (isNaN(weight)) {
        displayErrorIdMessage(errorElement, "숫자를 입력하세요.");
        return false;
    }
}


function calculateCalorieDog() {
    var weight = parseFloat(document.getElementById("WeightDog").value);
    var activeLevelDog = parseFloat(document.getElementById("activeLevelSelectDog").value);
    var errorElement = document.getElementById("errorDog");
    if (!isNaN(weight) && !isNaN(activeLevelDog)) {
        var RER = 0.0;

        if (weight > 2.0) {
            RER = 70 * (weight * 0.75);
        } else {
            RER = 30 * weight + 70;
        }

        var calorie = RER * activeLevelDog;
        document.getElementById("CalorieDog").value = calorie.toFixed(2) + " kcal";
        displayErrorIdMessage(errorElement, "");
    }
    else if (weight === "") {
        displayErrorIdMessage(errorElement, "몸무게를 입력하세요.");
        return false;
    }
    else if (isNaN(weight)) {
        displayErrorIdMessage(errorElement, "숫자를 입력하세요.");
        return false;
    }
}


function valid() {
    var Weight = document.forms[0]["Weight"].value;

    displayErrorIdMessage("");

    if (Weight === "") {
        displayErrorIdMessage("몸무게를 입력하세요");
    }
    else if (isNaN(Weight)) {
        displayErrorIdMessage("숫자로 입력하세요");
    }
}

// 오류 메시지 표시
function displayErrorIdMessage(errorElement, message) {
    errorElement.innerHTML = message;
}

