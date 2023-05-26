// 오류 메시지 표시
function displayErrorIdMessage(message, errorId) {
    var errorElement = document.getElementById(errorId);
    if (errorElement) {
        errorElement.innerHTML = message;
    }
}

function validateForm() {
    var title = document.querySelector('input[name="Title"]').value;
    var context = document.querySelector('textarea[name="Context"]').value;

    // 오류 메시지 초기화
    displayErrorIdMessage("", "validError1");
    displayErrorIdMessage("", "validError2");

    if (title.trim() === '' && context.trim() === '') {
        displayErrorIdMessage("제목을 입력하세요", "validError1");
        displayErrorIdMessage("내용을 입력하세요", "validError2");
        return false;
    }
    else if (title.trim() === '') {
        displayErrorIdMessage("제목을 입력하세요", "validError1");
        return false;
    }
    else if (context.trim() === '') {
        displayErrorIdMessage("내용을 입력하세요", "validError2");
        return false;
    }
}